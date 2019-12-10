using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori_algorithm.Models.Reader
{
    public interface IDataReader
    {
        List<ItemBasket> GetItems();
    }
}
