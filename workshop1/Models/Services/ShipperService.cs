using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using workshop1.Daos;

namespace workshop1.Models.Services
{
    public class ShipperService
    {

        /// <summary>
        /// 取得所有貨運商
        /// </summary>
        /// <returns></returns>
        public IList<Shipper> GetShippers()
        {
            ShippersDao dao = new ShippersDao();
            return dao.GetAllShippers();
        }
    }
}