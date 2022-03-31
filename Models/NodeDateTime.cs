using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NodeDateTime : IComparable<NodeDateTime>
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime MyDateTime { get; set; }
        public NodeDateTime(string brand, string model)
        {
            Brand = brand;
            Model = model;
            MyDateTime = DateTime.Now;
        }
        public NodeDateTime()
            :this("","")
        {

        }
        public int CompareTo(NodeDateTime other)
        {
            return MyDateTime.CompareTo(other.MyDateTime);
        }
        public override string ToString()
        {
            return $"Model - {Model} , bought at - {MyDateTime}.";
        }
    }
}
