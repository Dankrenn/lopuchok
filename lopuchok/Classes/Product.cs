using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lopuchok.Classes
{
    public class ProductType
    {
        public int ID { get; set; }
        public string Title { get; set; }
    }
    public class Product
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public ProductType ProductType { get; set; }
        public string ArticleNumber { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int ProductionPersonCount { get; set; }
        public int ProductionWorkshopNumber { get; set; }
        public double MinCostForAgent { get; set; }

        public List<Material> Materials { get; set; }
    }
}
