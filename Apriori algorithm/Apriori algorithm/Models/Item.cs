using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori_algorithm.Models
{
    public class Item
    {
        public string name { get; set; }

        public Item() {}

        public Item(string name)
        {
            this.name = name;
        }
    }
}
