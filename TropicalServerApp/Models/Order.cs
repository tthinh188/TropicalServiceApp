using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TropicalServerApp.Models
{
    public class Order
    {
        public string orderTracking { get; set; }
        public string OrderDate { get; set; }
        public int OrderCustomerNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public int CustomerRouteNumber { get; set; }
    }
}