using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStracture
{
    public class CycleTruncateQueue<T>:QueueWithArray<T>
    {
        int firstInd;
        int lastInd;
        T[] queueArr;

        public CycleTruncateQueue(int size = 10)
        {
            queueArr = new T[size];
            lastInd = firstInd = -1;
        }
        public override bool EnQueue(T item)
        {
            if (IsFull())
            {
                DeQueue(out T removedValue);
            }
            if (IsEmpty()) firstInd = 0;
            lastInd = (lastInd + 1) % queueArr.Length;
            queueArr[lastInd] = item;
            return true;
        }
    }
}
