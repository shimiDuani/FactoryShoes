using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStracture
{

    public class DoubleLinkedList<T> where T:IComparable<T>
    {
        public Node start = null;
        public Node end = null;

        public void AddFirst(T val)
        {
            Node n = new Node(val);
            if (start == null)
            {
                end = n;
            }
            else
            {
                start.previous = n;
            }
            n.next = start;
            start = n;
        }//insert to first
        public void AddLast(T val)
        {
            if (start == null)
            {
                AddFirst(val);
                return;
            }
            Node n = new Node(val);
            end.next = n;
            n.previous = end;
            end = end.next;
        }//insert to last
        public bool RemoveFirst(out T saveRemoveValue)
        {
            saveRemoveValue = default;
            if (start == null) return false;
            saveRemoveValue = start.value;
            start = start.next;
            if (start == null)
            {
                end = null;

            }
            else start.previous = null;

            return true;


        }
        public bool RemoveLast(out T saveRemoveValue)
        {
            saveRemoveValue = default;    // O(1)
            if (start == null) return false;
            saveRemoveValue = end.value;
            Node tmp = new Node(end.value);
            if (end.previous != null)
            {
                tmp.previous = end.previous;
                end.previous = null;
                end = tmp.previous;
                end.next = null;
            }
            else
            {
                start = null;
                end = null;
            }
            return true;

        } 
        public T this[int index]
        {
            get
            {
                Node tmp = start;
                int count = 0;
                if (tmp == null) return default;
                while (count != index)
                {
                    tmp = tmp.next;
                    count++;
                    if (tmp == null) return default;

                }
                return tmp.value;
            }

        } // GET(place) FUNC   
        public bool AddAt(int index, T value)
        {
            Node tmpStart = start;
            int count = 0;
            if (tmpStart == null)
            {
                AddFirst(value);
                return true;
            } //list is empty
            if (index == 0)
            {
                AddFirst(value);
                return true;
            } //enter first num when there is one num
            while (count < index - 1)
            {
                tmpStart = tmpStart.next;
                count++;
                if (tmpStart == null) return false;

            }
            Node addNode = new Node(value);
            if (tmpStart.next == null)
            {
                AddLast(value);
                return true;
            } //if it is the last number
            tmpStart.next.previous = addNode;
            addNode.next = tmpStart.next;
            tmpStart.next = addNode;
            addNode.previous = tmpStart;
            return true;

        } //set(place) 
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Node tmp = start;

            while (tmp != null)
            {
                sb.Append(tmp.value.ToString() + " ");
                tmp = tmp.next;
            }

            return sb.ToString();
        }
        public void MoveToEndByNode(Node nodeToMove)
        {
            if (nodeToMove == null) AddLast(nodeToMove.value);//If there's nothing on the linked list
            if (nodeToMove.next == null) return;//If there is one node
            if (nodeToMove.previous == null)//If its the first node
            {
                start = start.next;
                nodeToMove.next.previous = null;
                nodeToMove.next = null;//Severing the connection 
                AddLast(nodeToMove.value);
                return;
            }

            nodeToMove.next.previous = nodeToMove.previous;//Link the node after him to the node before it
            nodeToMove.previous.next = nodeToMove.next;
            nodeToMove.next = null;
            nodeToMove.previous = null;//Severing the connection 
            AddLast(nodeToMove.value); 
        }
        public class Node
        {
            public T value;
            public Node next;
            public Node previous;

            public Node(T value)
            {
                this.value = value;
                next = null;
            }
        }
    }
}
