using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using workshop1.Daos;

namespace workshop1.Models.Services
{
    public class CustomerService
    {
        /// <summary>
        /// 取得 CompnanyName by customerID
        /// </summary>
        /// <param name="customerID">客戶編號</param>
        /// <returns></returns>
        public string GetCompanyName(int customerID)
        {
            CustomersDao dao = new CustomersDao();

            List<Customer> customers =  dao.GetAllCustomers(); ;
            Customer customer = customers.SingleOrDefault(m => m.CustomerID == customerID);

            return (customer != null) ? customer.CompanyName : null;
        }

        /// <summary>
        /// 取得所有客戶資料
        /// </summary>
        /// <returns></returns>
        public IList<Customer> GetCustomers()
        {
            CustomersDao dao = new CustomersDao();

            return dao.GetAllCustomers(); ;
        }
    }
}