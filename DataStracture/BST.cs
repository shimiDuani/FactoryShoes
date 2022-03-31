using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStracture
{
    public class BST<T> where T : IComparable<T>
    {
        public Node root { get; set; }
        public virtual void Add(T val)
        {
            if (root == null)
            {
                root = new Node(val);
                return;
            }
            Node tmp = root;

            while (true)
            {
                if (val.CompareTo(tmp.value) < 0)  // if val < tmp.value  go left
                {
                    if (tmp.Left == null)
                    {
                        tmp.Left = new Node(val);
                        break;
                    }
                    else tmp = tmp.Left;
                }
                else  //go right
                {
                    if (tmp.Right == null)
                    {
                        tmp.Right = new Node(val);
                        break;
                    }
                    else tmp = tmp.Right;
                }
            }
            //if (val >= root.value) root.Right = new Node(val);
        }//Add new node
        public virtual void ScanInOrder(Action<string> act)
        {
            if (act == null) throw new ArgumentNullException("action function cant be null");
            ScanInOrder(root, act);
        }
        public  void ScanInOrder(Node tmp, Action<string> act)
        {
            if (tmp == null) return;
            ScanInOrder(tmp.Left, act);
            act?.Invoke($"{tmp.value}");
            ScanInOrder(tmp.Right, act);
        }
        public bool Search(T itemToFind, out T foundItem)
        {
            foundItem = default;
            if (root == default) return false;
            return Search1(itemToFind, out foundItem, root);
        }
        public bool Search1(T itemToFind, out T foundItem, Node node)
        {
            foundItem = default;
            if (node == null) return false;
            if (itemToFind.CompareTo(node.value) > 0) return Search1(itemToFind, out foundItem, node.Right);
            else
            {
                if (itemToFind.CompareTo(node.value) == 0)
                {
                    foundItem = node.value;
                    return true;
                }
                else
                {
                    return Search1(itemToFind, out foundItem, node.Left);
                }
            }
        }
        public bool RemoveNotRequ(T itemToRemove, out T itemRemoved)
        {
            Node tmp = root;
            itemRemoved = default;
            if (itemToRemove.CompareTo(tmp.value) == 0)
            {
                itemRemoved = tmp.value;
                RemoveNotRequNext(itemToRemove, tmp);
                return true;
            };
            while (true)
            {
                if (tmp == null) return false;
                if (tmp.Left != null && itemToRemove.CompareTo(tmp.Left.value) == 0)
                {
                    itemRemoved = tmp.Left.value;
                    if (tmp.Left.Left == null && tmp.Left.Right == null) tmp.Left = default; // leaf on left
                    else RemoveNotRequNext(itemToRemove, tmp.Left);
                    return true;
                }
                else
                {
                    if (tmp.Right != null && itemToRemove.CompareTo(tmp.Right.value) == 0)
                    {
                        itemRemoved = tmp.Right.value;
                        if (tmp.Right.Left == null && tmp.Right.Right == null) tmp.Right = default; //leaf on right
                        else RemoveNotRequNext(itemToRemove, tmp.Right);
                        return true;
                    }
                    if (itemToRemove.CompareTo(tmp.value) > 0) tmp = tmp.Right;
                    else tmp = tmp.Left;

                }
            }
        }
        private Node FindMaxDadRightLeftest(Node node)
        {
            node = node.Right;
            if (node.Left == null) return node;
            while (node.Left.Left != null)
            {
                node = node.Left;
            }
            return node;
        }
        private void RemoveNotRequNext(T itemToRemove, Node tmp)
        {
            if (tmp.Left != null && tmp.Right != null)
            {
                RemoveTwoChilds(itemToRemove, tmp);
                return;
            } // two child
            if (tmp.Left == null && tmp.Right != null)
            {
                tmp.value = tmp.Right.value;
                if (tmp.Right.Right == null) tmp.Right = null;
                else
                {
                    tmp.Right = tmp.Right.Right;
                }
                return;
            } // one child on the right
            if (tmp.Left != null && tmp.Right == null)
            {
                tmp.value = tmp.Left.value;
                tmp.Left = null;
                return;
            }  // one child on the left
            if (tmp.Left == null && tmp.Right == null)
            {
                root = null;
            }
        }
        private void RemoveTwoChilds(T itemToRemove, Node tmp)
        {
            Node Max = FindMaxDadRightLeftest(tmp);
            if (Max.Left == null)
            {
                if (Max.Right == null)
                {
                    tmp.value = Max.value;
                    tmp.Right = default;
                }
                else
                {
                    tmp.value = Max.value;
                    tmp.Right = Max.Right;
                }

            }
            else
            {
                if (Max.Left.Right != default)
                {
                    tmp.value = Max.Left.value;
                    Max.Left = Max.Left.Right;
                }
                else
                {
                    tmp.value = Max.Left.value;
                    Max.Left = default;
                }
            }

        }
        public Node Root() => root;
        public T FindClosestValue(T Target)
        {
            return FindClosestValue(root, Target);
        }
        private T FindClosestValue(Node No, T Target)
        {
            T Closest = No.value;//value of node closest
            Node CurrntNode = No;//the current node
            while (true)
            {
                T NodeVal = CurrntNode.value;//value current node
                if (Math.Abs(Target.CompareTo(Closest)) > Math.Abs(Target.CompareTo(NodeVal)))//val closest vs current value node
                {
                    Closest = NodeVal;//take the closest
                }
                if (Target.CompareTo(NodeVal) > 0)
                {
                    if (CurrntNode.Right == default) break;
                    CurrntNode = CurrntNode.Right;
                }
                else if (Target.CompareTo(NodeVal) < 0)
                {
                    if (CurrntNode.Left == default) break;
                    CurrntNode = CurrntNode.Left;
                }
                else
                {
                    break;
                }
            }
            return Closest;
        }//found closest value
        public class Node
        {
            public T value;
            public Node Left;
            public Node Right;
            public Node(T value)
            {
                this.value = value;
            }
        }
    }
}
   