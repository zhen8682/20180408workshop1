using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using workshop1.Models;

namespace workshop1.Daos
{
    public class ProductsDao : BaseDao
    {
        /// <summary>
        /// 取得所有產品
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProduct()
        {
            List<Product> result = new List<Product>();
            using (SqlConnection conn = GetSqlConnection())
            {
                string sql = "select * from Production.Products";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                DataSet ds = new DataSet();
                adapter.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(new Product
                    {
                        ProductID = int.Parse(row["ProductID"].ToString()),
                        ProductName = row["ProductName"].ToString(),
                        UnitPrice = decimal.Parse(row["UnitPrice"].ToString()),
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// 取得產品單價
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public decimal GetPrice(int productId)
        {
            using (SqlConnection conn = GetSqlConnection())
            {
                string sql = @"
                    select UnitPrice from Production.Products where ProductID = @id
                ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@id", productId));

                conn.Open();
                decimal result = Convert.ToDecimal(cmd.ExecuteScalar());
                conn.Close();
                return result;
            }
        }
    }
}