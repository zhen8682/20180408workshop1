using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20180329workshop1.Models
{
    public class Orders
    {
        public int OrderID { get; set; }

        public int CustomerID { get; set; }

        public int EmployeeID { get; set; }

        /// <summary>
        ///         特殊欄位OrderDate DATETIME ;RequiredDate DATETIME ;ShippedDate DATETIME;Freight MONEY;
        /// </summary>
        
        public Nullable<DateTime> OrderDate { get; set; }

        public Nullable<DateTime> RequiredDate { get; set; }

        public Nullable<DateTime> ShippedDate { get; set; }

        public Nullable<Decimal> Freight { get; set; }

        public int ShipperID { get; set; }

        public string ShipAddress { get; set; }

        public string ShipCity { get; set; }

        public string ShipRegion { get; set; }

        public string ShipPostalCode { get; set; }

        public string ShipCountry { get; set; }

        
    }




}

