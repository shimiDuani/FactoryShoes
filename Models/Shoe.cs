using DataStracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataStracture.DoubleLinkedList<Models.NodeDateTime>;

namespace Models
{
    public class Shoe
    {
        public Node DateTimeNode{ get; set; }

        private CycleTruncateQueue<NodeDateTime> incidencesShoes;
        public CycleTruncateQueue<NodeDateTime> IncidencesShoes { get; set; }//Queue of last purchase dates
        public string Brand { get; set; }
        public string Model { get; set; }
        public BST<SizeAndAmount> BstSizeAndAmount { get; set; }
        public Shoe()
        {
            NodeDateTime dateTime = new NodeDateTime(Brand,Model);
            DateTimeNode = new Node(dateTime);
            BstSizeAndAmount = new BST<SizeAndAmount>();
            IncidencesShoes = new CycleTruncateQueue<NodeDateTime>();
        }
    }
}
