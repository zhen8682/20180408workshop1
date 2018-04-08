using _20180329workshop1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _20180329workshop1.Controllers
{
    public class HomeController : Controller
    {
        public List<Models.Orders> GetOrdersBycondition(Models.OrdersSearchArg arg)
        {
            List<Models.Orders> result = new List<Orders>();

            ///OrderDate = "2018/04/07", RequiredDate = "2018/04/07", ShippedDate = "2018-04-07"

            result.Add(new Orders() { OrderID = 1, CustomerID = 1, EmployeeID = 1, Freight = 60, ShipperID = 1, ShipAddress = "台南市忠孝東路21號", ShipCity = "台南市", ShipRegion = "東區", ShipPostalCode = "701", ShipCountry = "台灣" });

            return result;
        }

        // GET: Home
        public ActionResult Index()
        {
            Models.OrdersService ordersservice = new Models.OrdersService();
            var orders = ordersservice.GetOrdersBycondition(new Models.OrdersSearchArg() {
                ///FirstName;LastName;
                OrderID = "1",
                ContactName="王曉明",
                CompanyName="科技公司",
                OrderDate="",
                ShippedDate="" 
            });

            ViewBag.OrdersAdd = orders[0];
            return View(orders);
            
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
            List<Models.Orders> result = new List<Orders>();
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