using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

using System.Text;
using System.Threading.Tasks;

namespace Apriori_algorithm.Models
{
    public class Apriori
    {
        private double _support;
        public double Support {
            get => _support * itemBaskets.Count();
            set => _support = value;
        }
        public double confidence { get; set; }
        public double lift { get; set; }

        private int iteration;

        public List<ItemBasket> itemBaskets = new List<ItemBasket>();

        public List<Hashtable> itemHistory;
        
        private Hashtable itemValues;
        public Apriori(List<ItemBasket> itemBaskets, double support, double confidence)
        {
            itemHistory = new List<Hashtable>();
            iteration = 1;
            this.itemBaskets = itemBaskets;
            this.Support = support;
            this.confidence = confidence;
            Start();
        }

        public void Start()
        {
            itemValues = ExtractUniqueItems(itemBaskets);
            itemValues = ExportItemCounts(itemBaskets, itemValues);
            AddToHistory(itemValues);
            RemoveUnsupported(itemValues);
            while (itemValues.Count > 1)
            {
                itemValues = ExtractItemSet(HashtableToList(itemValues));
                //itemValues = ExportItemCounts(itemBaskets, itemValues);
                //itemValues = FindContainedItems(itemValues);
                RemoveUnsupported(itemValues);
                AddToHistory(itemValues);
            }
        }

        private Hashtable ExtractUniqueItems(List<ItemBasket> fullItems)
        {
            Hashtable hashtable = new Hashtable();
            foreach(ItemBasket ib in itemBaskets)
            {
                foreach(Item item in ib.items)
                {
                    if(!hashtable.Contains((string)item.name))
                        hashtable.Add(item.name, 0);
                }
            }
            return hashtable;
        }

        private void AddToHistory(Hashtable hashtable)
        {
            Hashtable saveTable = new Hashtable();
            foreach(DictionaryEntry de in hashtable)
            {
                saveTable.Add(de.Key, de.Value);
            }
            itemHistory.Add(saveTable);
        }

        //exporting items to single table
        private Hashtable ExportItemCounts(List<ItemBasket> itemBaskets, Hashtable items)
        {
            foreach(ItemBasket basket in itemBaskets)
            {
                foreach(Item item in basket.items)
                {
                    if(items.Contains(item.name))
                    {
                        items[item.name] = (int)items[item.name] + 1;
                    }
                }
            }
            //clear empty hash
            if (items.Contains(""))
                items.Remove("");

            return items;
        }

        private Hashtable FindContainedItems(Hashtable table)
        {
            foreach(ItemBasket ib in itemBaskets)
            {
                ib.ContainsItems(table);
            }
            return table;
        }

        private void RemoveUnsupported(Hashtable itemTable)
        {
            var itemsToRemove = new List<string>();
            foreach(DictionaryEntry item in itemTable)
            {
                if ((int)item.Value < Support)
                    itemsToRemove.Add((string)item.Key);
            }

            foreach (string key in itemsToRemove)
                itemTable.Remove(key);
        }

        private List<string> HashtableToList(Hashtable table)
        {
            var list = new List<string>();
            foreach (DictionaryEntry de in table)
                list.Add((string)de.Key);
            return list;
        }

        private Hashtable ExtractItemSet(List<string> items)
        {
            var currentItems = new Hashtable();
            //first iteration
            if(iteration == 1)
            {
                for(int i = 0; i < items.Count; i++)
                {
                    for(int x = i+1; x < items.Count; x++)
                    {
                        currentItems.Add(items[i] + items[x], 0);
                    }
                }
            }
            //other iterations
            else
            {
                for(int i = 0; i < items.Count; i++)
                {
                    for(int x = i+1; x < items.Count; x++)
                    {
                        var staticHalf = items[i].Substring(0, iteration - 1);
                        var dynamicHalf = items[x].Substring(0, iteration - 1);
                        if (staticHalf == dynamicHalf)
                        {
                            currentItems.Add(items[i]+items[x].Substring(iteration-1), 0);
                        }
                    }
                }
            }
            iteration++;
            return currentItems;
        }
    }
}