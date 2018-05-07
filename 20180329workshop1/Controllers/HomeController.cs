using _20180329workshop1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;


namespace _20180329workshop1.Controllers
{
    public class HomeController : Controller
    {
        

           List<Models.Orders> result = new List<Orders>();
        public  HomeController(){

            
             


        }

        //public static List<Models.Orders> GetOrdersBycondition(Models.OrdersSearchArg arg)
        //{
            

        //    ///OrderDate = "2018/04/07", RequiredDate = "2018/04/07", ShippedDate = "2018-04-07"

           
        //    return result;

        //}

        // GET: Home
        public ActionResult Index()
        {
            String connStr = @"Data Source=DESKTOP-KB2KNKQ\SQL2012;Initial Catalog=TSQL2012;Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(connStr);
            string sql = "SELECT * FROM [Sales].[Orders]";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DataTable select = ds.Tables[0];

            List<Models.Orders> result = new List<Orders>();
            foreach (DataRow item in select.Rows)
            {
                Nullable<DateTime> OrderDate = null;
                Nullable<DateTime> RequiredDate = null;
                Nullable<DateTime> ShippedDate = null;
                if (item["OrderDate"].ToString().Length>0)
                {OrderDate = Convert.ToDateTime(item["OrderDate"].ToString());
                }
                if (item["RequiredDate"].ToString().Length > 0)
                {
                    RequiredDate = Convert.ToDateTime(item["RequiredDate"].ToString());
                }
                if (item["ShippedDate"].ToString().Length > 0)
                {
                    ShippedDate = Convert.ToDateTime(item["ShippedDate"].ToString());
                }
                Models.Orders model = new Orders
                {
                    OrderID = int.Parse(item["OrderID"].ToString()),
                    CustomerID = int.Parse(item["CustomerID"].ToString()),
                    EmployeeID = int.Parse(item["EmployeeID"].ToString()),
                    ShipperID = int.Parse(item["ShipperID"].ToString()),
                    Freight = Decimal.Parse(item["Freight"].ToString()),
                    OrderDate = OrderDate,
                    RequiredDate = RequiredDate,
                    ShippedDate = ShippedDate,
                    ShipAddress = item["ShipAddress"].ToString(),
                    ShipCity = item["ShipCity"].ToString(),
                    ShipRegion = item["ShipRegion"].ToString(),
                    ShipPostalCode = item["ShipPostalCode"].ToString(),
                    ShipCountry = item["ShipCountry"].ToString()
                };
                result.Add(model);
            }
            ViewBag.list = result;
            return View();




            //Models.OrdersService ordersservice = new Models.OrdersService();
            //var orders = ordersservice.GetOrdersBycondition(new Models.OrdersSearchArg() {
            //    ///FirstName;LastName;
            //    OrderID = "1",
            //    ContactName="王曉明",
            //    CompanyName="科技公司",
            //    OrderDate="",
            //    ShippedDate="" 
            //});

            //ViewBag.OrdersAdd = orders[0];
            /////return View(orders);
            
        }

        public ActionResult Create()
        {
            Models.Orders orders = new Models.Orders();
            orders.CustomerID = 2;
            orders.CustomerID = 2;
            orders.Freight = 70;
            orders.OrderID = 2;
            orders.ShipAddress = "中正路3號";
            orders.ShipCity = "台北市";
            orders.ShipCountry = "台灣";
            orders.ShipperID = 2;
            orders.ShipPostalCode = "701";
            orders.ShipRegion = "中正區";

            List<SelectListItem> empdata = new List<SelectListItem>();
            empdata.Add(new SelectListItem()
            {
                Value = "1",
                Text = "王曉明"
            });

            empdata.Add(new SelectListItem()
            {
                Value = "2",
                Text = "陳春嬌"
            });

            ViewBag.empdata = empdata;

            return View(orders);
        }

        [HttpPost]
        public ActionResult Create(Models.Orders orders)
        {
            
            List<SelectListItem> empdata = new List<SelectListItem>();
            empdata.Add(new SelectListItem()
            {
                Value = "1",
                Text = "王曉明"
            });

            empdata.Add(new SelectListItem()
            {
                Value = "2",
                Text = "陳春嬌"
            });

            ViewBag.empdata = empdata;

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(orders);
        }

        public ActionResult Search()
        {
            List<SelectListItem> empdata = new List<SelectListItem>();
            empdata.Add(new SelectListItem()
            {
                Value = "1",
                Text = "王曉明"
            });

            empdata.Add(new SelectListItem()
            {
                Value = "2",
                Text = "陳春嬌"
            });

            ViewBag.empdata = empdata;

            List<SelectListItem> shipcom = new List<SelectListItem>();
            shipcom.Add(new SelectListItem()
            {
                Value = "1",
                Text = "高應大公司"
            });

            shipcom.Add(new SelectListItem()
            {
                Value = "2",
                Text = "高科大公司"
            });

            ViewBag.shipcom = shipcom;
            return View();
        }

        [HttpPost()]
        public ActionResult Search(FormCollection form)
        {
            
            return View();
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id)
        {
            
            //IEnumerable <string> results = result.Where
            //if (id == null)
            //{
            //    return HttpNotFound();
            //}
            /// var abc = List<Models.Orders>.Find(id);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Models.Orders orders)
        { 
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index");
            }
            return View(orders);
        }

        public ActionResult Delete(int id = 0)
        {
            //item item = db.item.Find(id);
            Models.Orders orders = new Models.Orders();
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        //
        // POST: /Home/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //item item = db.item.Find(id);
            
            return RedirectToAction("Index");
        }

    }
}
