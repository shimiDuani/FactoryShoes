using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DistributionPiont : IComparable<DistributionPiont>, IComparer<DistributionPiont>
    {
        public int ZipCode { get; set; }
        public string MyName { get; set; }
        public int Incidence { get; set; }
        public DistributionPiont(string myName, int zipCode)
        {
            MyName = myName;
            ZipCode = zipCode;
        }
        public DistributionPiont()
            : this("", 0)
        {

        }
        public int CompareTo(DistributionPiont other)
        {
            return ZipCode - other.ZipCode;
        }
        public int Compare(DistributionPiont x, DistributionPiont y)
        {
            return x.Incidence.CompareTo(y.Incidence);
        }
        public override string ToString()
        {
            return $"The name - {MyName} , ZipCode - {ZipCode}, and the number of orders from this point is {Incidence}";
        }
    }
}
