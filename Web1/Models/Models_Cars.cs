using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web1.Models
{
    public class Models_Cars
    {
        public int ModelId { get; set; }
        public string ModelName { get; set;  }
        public string Type_Chasi { get; set; }

        public string DateOfProduction { get; set; }

        public string PhotoFileName { get; set; }

    }
}