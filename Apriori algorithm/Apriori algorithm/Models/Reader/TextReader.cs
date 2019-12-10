using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori_algorithm.Models.Reader
{
    class TextReader : IDataReader
    {
        string filePath = "./data.txt";

        public List<ItemBasket> GetItems()
        {
            if (File.Exists(filePath))
            {
                List<ItemBasket> itemsList = new List<ItemBasket>();
                string[] lines = File.ReadAllLines(filePath);
                foreach(string itemList in lines)
                {
                    List<Item> items = new List<Item>();
                    string[] listItems = itemList.Split(' ');
                    foreach (string individualItem in listItems)
                        items.Add(new Item(individualItem));
                    itemsList.Add(new ItemBasket(items));
                }

                return itemsList;
            } else
            {
                throw new Exception("No data file found");
                return null;
            }
        }
    }
}
