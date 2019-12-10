using Apriori_algorithm.Models;
using Apriori_algorithm.Models.Reader;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Apriori_algorithm.ViewModels
{
    class MainViewModel : Screen
    {
        public MainViewModel()
        {
            var tr = new TextReader();
            Apriori apriori = new Apriori(tr.GetItems(), 0.2, 0.2);
            Console.WriteLine();
        }
    }
}
