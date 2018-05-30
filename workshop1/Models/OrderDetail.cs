using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshop1.Models
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
    }
}