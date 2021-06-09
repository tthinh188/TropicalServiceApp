using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TropicalServerApp.Models
{
    public class Product
    {
        public int itemNumber { get; set; }
        public string itemDescription { get; set; }
        public string prePrice { get; set; }
        public double itemWeights { get; set; }
    }
}