using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SizeAndAmount : IComparable<SizeAndAmount>
    {
        public float Size { get; set; }
        public int Amount { get; set; }
        public SizeAndAmount(float size, int amount)
        {
            Size = size;
            Amount = amount;
        }
        public SizeAndAmount()
            : this(0, 0)
        {

        }
        public int CompareTo(SizeAndAmount other)
        {
            return Size.CompareTo(other.Size);
        }
        public override string ToString()
        {
            return $"Size - {Size}, Amount - {Amount}";
        }
    }
}
