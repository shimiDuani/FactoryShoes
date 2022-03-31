using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStracture
{

    public class QueueWithArray<T> : IEnumerable<T>
    {
        int firstInd;
        int lastInd;
        T[] queueArr;

        public QueueWithArray(int size = 30)
        {
            queueArr = new T[size];
            lastInd = firstInd = -1;
        }

        public virtual bool EnQueue(T item) // insert
        {
            if (IsFull()) return false;
            if (IsEmpty()) firstInd = 0;

            lastInd = (lastInd + 1) % queueArr.Length;
            //lastInd = lastInd + 1;
            //if(lastInd == queueArr.Lenght) lastInd = 0;
            queueArr[lastInd] = item;
            return true;
        }

        public bool DeQueue(out T removedValue)
        {
            removedValue = default;
            if (IsEmpty()) return false;

            removedValue = queueArr[firstInd];
            if (firstInd == lastInd) firstInd = lastInd = -1;// removing last item
            else firstInd = (firstInd + 1) % queueArr.Length;
            return true;
        }

        public bool IsEmpty() => firstInd == -1;
        public bool IsFull() => (lastInd + 1) % queueArr.Length == firstInd;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            int tmp = firstInd;
            while (tmp != lastInd)
            {
                sb.Append(queueArr[tmp] + " ");
                tmp = (tmp + 1) % queueArr.Length;
            }
            if (!IsEmpty()) sb.Append(queueArr[tmp] + " ");
            return sb.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            int tmp = firstInd;
            if (IsEmpty()) yield break;

            while (tmp != lastInd)
            {
                yield return queueArr[tmp];
                tmp = (tmp + 1) % queueArr.Length;
            }

            yield return queueArr[tmp];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
