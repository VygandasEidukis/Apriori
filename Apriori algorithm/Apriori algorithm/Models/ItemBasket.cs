using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori_algorithm.Models
{
    public class ItemBasket
    {
        public List<Item> items { get; set; }

        public ItemBasket() { }

        public ItemBasket(List<Item> items)
        {
            this.items = items;
        }

        public bool ContainsItems(Hashtable containedItems)
        {
            for(int i = 0; i < items.Count(); i++)
            {
                if(containedItems.Contains(items[i]))
                {
                    //check if items exist in basket
                    containedItems = NullifyHashtable(containedItems);
                    for(int x = i; x < containedItems.Count; x++)
                    {
                        if (containedItems.Contains(items[x]))
                            containedItems[items[x]] = 1;
                    }

                    //if items exist, returns true value
                    if (ValidateContainingSet(containedItems))
                        return true;
                }
            }
            return false;
        }

        private bool ValidateContainingSet(Hashtable hashtable)
        {
            foreach(DictionaryEntry de in hashtable)
            {
                if ((int)de.Value != 1)
                    return false;
            }
            return true;
        }

        private Hashtable NullifyHashtable(Hashtable table)
        {
            Hashtable hashtable = new Hashtable();
            foreach(DictionaryEntry de in table)
            {
                hashtable.Add(de.Key, 0);
            }
            return hashtable;
        }
    }
}
