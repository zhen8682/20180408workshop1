using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20180329workshop1.Models
{
    public class OrdersSearchArg
    {
        public string OrderID { get; set; }

        public string ContactName { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string CompanyName { get; set; }

        public string OrderDate { get; set; }

        public string ShippedDate { get; set; }
        

    }
}