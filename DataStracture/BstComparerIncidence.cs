using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStracture
{
   public class BstComparerIncidence<T>:BST<T> where T : IComparable<T>,IComparer<T>
    {
        public override void Add(T val)
        {
            if (root == null)
            {
                root = new Node(val);
                return;
            }
            Node tmp = root;

            while (true)
            {
                if (val.Compare(val,tmp.value) < 0) 
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
        }//add by comparer 
        public void Bst(BST<T> bstPoints)
        {
            BstAddInOrder(bstPoints.root);//Moving from tree by zip code to tree according to the incidence of distribution points
        }
        private void BstAddInOrder(Node root)
        {
            if (root == null) return;
            BstAddInOrder(root.Left);
            Add(root.value);
            BstAddInOrder(root.Right);
        }//add node to in order
        public override void ScanInOrder(Action<string> act)
        {
            if (act == null) throw new ArgumentNullException("action function cant be null");
            BstInOrder(root, act);
        }
        private void BstInOrder(Node root, Action<string> action)//InOrder
        {
            if (root == null) return;
            BstInOrder(root.Right, action);
            action?.Invoke($"{root.value}");
            BstInOrder(root.Left, action);
        }
    }
}
