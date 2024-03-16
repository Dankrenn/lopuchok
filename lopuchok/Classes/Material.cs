using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lopuchok.Classes
{
    public class MaterialType
    {
        public int ID { get; set; }
        public string Title { get; set; }
    }
    public class Material
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public MaterialType MaterialType { get; set; }
    }
}
