using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20180329workshop1.Models
{
    /// <summary>
    /// 訂單crud服務
    /// </summary>
    public class OrdersService
    {
        /// <summary>
        /// 訂單新增
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public int InsertOrders(Models.Orders orders) {
            return 0;
        }

        public List<Models.Orders> GetOrdersBycondition(Models.OrdersSearchArg arg) {
            List<Models.Orders> result = new List<Orders>();
            
            ///OrderDate = "2018/04/07", RequiredDate = "2018/04/07", ShippedDate = "2018-04-07"
            
            result.Add(new Orders() { OrderID = 1, CustomerID = 1, EmployeeID = 1, Freight = 60, ShipperID = 1, ShipAddress = "台南市忠孝東路21號", ShipCity = "台南市", ShipRegion = "東區", ShipPostalCode = "701", ShipCountry = "台灣" });

            return result;
        }
    }
}