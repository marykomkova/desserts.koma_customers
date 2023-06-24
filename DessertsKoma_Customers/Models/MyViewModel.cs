using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertsKoma_Customers.Models
{
    public class MyViewModel
    {
        public List<ТипыДесертов> TypesModel { get; set; }
        public List<Десерты> DessertsModel { get; set; }
        public List<int> TypesCountsModel { get; set; }
        public double MinCostModel { get; set; }
        public double MaxCostModel { get; set; }
        public int AllDessertsCount { get; set; }
    }
}
