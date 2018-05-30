using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using workshop1.Models;

namespace workshop1.Daos
{
    public class ShippersDao : BaseDao
    {
        public List<Shipper> GetAllShippers()
        {
            List<Shipper> result = new List<Shipper>();
            using (SqlConnection conn = GetSqlConnection())
            {
                string sql = "select  * from Sales.Shippers";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                DataSet ds = new DataSet();
                adapter.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(new Shipper
                    {
                        ShipperID = int.Parse(row["ShipperID"].ToString()),
                        CompanyName = row["CompanyName"].ToString(),
                        Phone = row["Phone"].ToString(),
                    });
                }
            }
            return result;
        }
    }
}