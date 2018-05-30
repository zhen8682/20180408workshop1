using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using workshop1.Daos;

namespace workshop1.Models.Services
{
    public class ProductService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Product> GetAllProduct()
        {
            ProductsDao dao = new ProductsDao();
            return dao.GetAllProduct();
        }

        public decimal GetPrice(int productID)
        {
            ProductsDao dao = new ProductsDao();
            return dao.GetPrice(productID);
        }
    }
}