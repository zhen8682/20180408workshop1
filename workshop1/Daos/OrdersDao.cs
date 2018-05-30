using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using workshop1.Models;

namespace workshop1.Daos
{
    public class OrdersDao : BaseDao
    {
        /// <summary>
        /// 取得所有的訂單
        /// </summary>
        /// <returns></returns>
        public List<Order> GetAllOrders()
        {
            List<Order> result = new List<Order>();
            using (SqlConnection conn = GetSqlConnection())
            {
                string sql = "select * from Sales.Orders";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                DataSet ds = new DataSet();
                adapter.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(new Order
                    {
                        OrderID = int.Parse(row["OrderID"].ToString()),
                        CustomerID = int.Parse(row["CustomerID"].ToString()),
                        EmployeeID = int.Parse(row["EmployeeID"].ToString()),

                        OrderDate = DateTime.Parse(row["OrderDate"].ToString()),
                        RequiredDate = DateTime.Parse(row["RequiredDate"].ToString()),
                        ShippedDate = (!string.IsNullOrWhiteSpace(row["ShippedDate"].ToString())) ? new DateTime?(DateTime.Parse(row["ShippedDate"].ToString())) : null,
                        ShipperID = (!string.IsNullOrWhiteSpace(row["ShipperID"].ToString())) ? new int?(int.Parse(row["ShipperID"].ToString())) : null,
                        Freight = (!string.IsNullOrWhiteSpace(row["Freight"].ToString())) ? new decimal?(decimal.Parse(row["Freight"].ToString())) : null,
                        ShipCountry = row["ShipCountry"].ToString(),
                        ShipCity = row["ShipCity"].ToString(),
                        ShipRegion = row["ShipRegion"].ToString(),
                        ShipPostalCode = row["ShipPostalCode"].ToString(),
                        ShipAddress = row["ShipAddress"].ToString()
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// 用訂單編號取得訂單及明細
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Order GetOrderById(int orderId)
        {
            using (SqlConnection conn = GetSqlConnection())
            {
                string sql = "select * from Sales.Orders where OrderID = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@Id", orderId));

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adapter.Fill(ds);

                DataRow row = ds.Tables[0].Rows[0];
                Order order =
                new Order
                {
                    OrderID = int.Parse(row["OrderID"].ToString()),
                    CustomerID = int.Parse(row["CustomerID"].ToString()),
                    EmployeeID = int.Parse(row["EmployeeID"].ToString()),

                    OrderDate = DateTime.Parse(row["OrderDate"].ToString()),
                    RequiredDate = DateTime.Parse(row["RequiredDate"].ToString()),
                    ShippedDate = (!string.IsNullOrWhiteSpace(row["ShippedDate"].ToString())) ? new DateTime?(DateTime.Parse(row["ShippedDate"].ToString())) : null,
                    ShipperID = (!string.IsNullOrWhiteSpace(row["ShipperID"].ToString())) ? new int?(int.Parse(row["ShipperID"].ToString())) : null,
                    Freight = (!string.IsNullOrWhiteSpace(row["Freight"].ToString())) ? new decimal?(decimal.Parse(row["Freight"].ToString())) : null,
                    ShipCountry = row["ShipCountry"].ToString(),
                    ShipCity = row["ShipCity"].ToString(),
                    ShipRegion = row["ShipRegion"].ToString(),
                    ShipPostalCode = row["ShipPostalCode"].ToString(),
                    ShipAddress = row["ShipAddress"].ToString()
                };




                string sqlDetail = "select * from Sales.OrderDetails where OrderID = @Id";
                SqlCommand cmdDetail = new SqlCommand(sqlDetail, conn);
                cmdDetail.Parameters.Add(new SqlParameter("@Id", orderId));

                SqlDataAdapter adapterDetail = new SqlDataAdapter(cmdDetail);

                DataSet dsDetail = new DataSet();
                adapterDetail.Fill(dsDetail);
                order.Details = new List<OrderDetail>();
                foreach (DataRow detailRow in dsDetail.Tables[0].Rows)
                {
                    order.Details.Add(new OrderDetail
                    {
                        OrderID = int.Parse(detailRow["OrderID"].ToString()),
                        ProductID = int.Parse(detailRow["ProductID"].ToString()),
                        UnitPrice = decimal.Parse(detailRow["UnitPrice"].ToString()),
                        Qty = int.Parse(detailRow["Qty"].ToString()),
                    });
                }

                return order;
            }
        }

        /// <summary>
        /// 新增訂單並回傳訂單編號
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        public int AddNewOrderReturnNewOrderId(Order newOrder)
        {
            SqlConnection conn = GetSqlConnection();
            string sql = @"
                INSERT INTO [Sales].[Orders]
                            ([CustomerID]
                            ,[EmployeeID]
                            ,[OrderDate]
                            ,[RequiredDate]
                            ,[ShippedDate]
                            ,[ShipperID]
                            ,[Freight]
                            ,[ShipName]
                            ,[ShipAddress]
                            ,[ShipCity]
                            ,[ShipRegion]
                            ,[ShipPostalCode]
                            ,[ShipCountry])
                        VALUES
                            (@CustomerID
                            ,@EmployeeID
                            ,@OrderDate
                            ,@RequiredDate
                            ,@ShippedDate
                            ,@ShipperID
                            ,@Freight
                            ,@ShipName
                            ,@ShipAddress
                            ,@ShipCity
                            ,@ShipRegion
                            ,@ShipPostalCode
                            ,@ShipCountry)
                    SELECT SCOPE_IDENTITY()
                ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@CustomerID", newOrder.CustomerID));
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", newOrder.EmployeeID));
            cmd.Parameters.Add(new SqlParameter("@OrderDate", newOrder.OrderDate));
            cmd.Parameters.Add(new SqlParameter("@RequiredDate", newOrder.RequiredDate));
            cmd.Parameters.Add(new SqlParameter("@ShippedDate", newOrder.ShippedDate.HasValue ? newOrder.ShippedDate.Value.ToString("yyyy/MM/dd") : ""));
            cmd.Parameters.Add(new SqlParameter("@ShipperID", newOrder.ShipperID));
            cmd.Parameters.Add(new SqlParameter("@Freight", newOrder.Freight));
            cmd.Parameters.Add(new SqlParameter("@ShipName", ""));
            cmd.Parameters.Add(new SqlParameter("@ShipAddress", newOrder.ShipAddress));
            cmd.Parameters.Add(new SqlParameter("@ShipCity", newOrder.ShipCity));
            cmd.Parameters.Add(new SqlParameter("@ShipRegion", newOrder.ShipRegion ?? ""));
            cmd.Parameters.Add(new SqlParameter("@ShipPostalCode", newOrder.ShipPostalCode ?? ""));
            cmd.Parameters.Add(new SqlParameter("@ShipCountry", newOrder.ShipCountry));

            int orderId;

            conn.Open();
            // 開啟交易控管
            SqlTransaction transaction = conn.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                orderId = Convert.ToInt32(cmd.ExecuteScalar());
                string sqlDetail = @"
                    INSERT INTO Sales.OrderDetails
                               ([OrderID]
                               ,[ProductID]
                               ,[UnitPrice]
                               ,[Qty]
                               ,[Discount])
                         VALUES
                               (@OrderID
                               ,@ProductID
                               ,@UnitPrice
                               ,@Qty
                               , 0)
                    ";
                foreach (var detail in newOrder.Details)
                {
                    if (detail.ProductID == 0)
                        continue;
                    SqlCommand cmdDetail = new SqlCommand(sqlDetail, conn);
                    cmdDetail.Transaction = transaction;
                    cmdDetail.Parameters.Add(new SqlParameter("@OrderID", orderId));
                    cmdDetail.Parameters.Add(new SqlParameter("@ProductID", detail.ProductID));
                    cmdDetail.Parameters.Add(new SqlParameter("@UnitPrice", detail.UnitPrice));
                    cmdDetail.Parameters.Add(new SqlParameter("@Qty", detail.Qty));
                    cmdDetail.ExecuteNonQuery();
                }
                // 全部動作做完後執行Commit
                transaction.Commit();
            }
            catch (Exception)
            {
                // 有出問題則將此交易內的所有更動的資料Rollback
                transaction.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }

            return orderId;
        }

        /// <summary>
        /// 更新訂單
        /// </summary>
        /// <param name="oldOrder"></param>
        public void UpdateOrder(Order oldOrder)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                SqlConnection conn = GetSqlConnection();
                // 更新主檔並刪除明細
                string sql = @"
                    UPDATE [Sales].[Orders]
                    SET 
                        CustomerID = @CustomerID
                        ,EmployeeID = @EmployeeID
                        ,OrderDate = @OrderDate
                        ,RequiredDate = @RequiredDate
                        ,ShippedDate = @ShippedDate
                        ,ShipperID = @ShipperID
                        ,Freight = @Freight
                        ,ShipName = @ShipName
                        ,ShipAddress = @ShipAddress
                        ,ShipCity = @ShipCity
                        ,ShipRegion = @ShipRegion
                        ,ShipPostalCode = @ShipPostalCode
                        ,ShipCountry = @ShipCountry
                    WHERE OrderID = @OrderID
            
                    DELETE FROM Sales.OrderDetails WHERE OrderID = @OrderID
                ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderID", oldOrder.OrderID));
                cmd.Parameters.Add(new SqlParameter("@CustomerID", oldOrder.CustomerID));
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", oldOrder.EmployeeID));
                cmd.Parameters.Add(new SqlParameter("@OrderDate", oldOrder.OrderDate));
                cmd.Parameters.Add(new SqlParameter("@RequiredDate", oldOrder.RequiredDate));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", oldOrder.ShippedDate.HasValue ? oldOrder.ShippedDate.Value.ToString("yyyy/MM/dd") : ""));
                cmd.Parameters.Add(new SqlParameter("@ShipperID", oldOrder.ShipperID));
                cmd.Parameters.Add(new SqlParameter("@Freight", oldOrder.Freight));
                cmd.Parameters.Add(new SqlParameter("@ShipName", ""));
                cmd.Parameters.Add(new SqlParameter("@ShipAddress", oldOrder.ShipAddress));
                cmd.Parameters.Add(new SqlParameter("@ShipCity", oldOrder.ShipCity));
                cmd.Parameters.Add(new SqlParameter("@ShipRegion", oldOrder.ShipRegion ?? ""));
                cmd.Parameters.Add(new SqlParameter("@ShipPostalCode", DBNull.Value/*oldOrder.ShipPostalCode ?? ""*/));
                cmd.Parameters.Add(new SqlParameter("@ShipCountry", oldOrder.ShipCountry));

                conn.Open();
                cmd.ExecuteNonQuery();

                int orderId = Convert.ToInt32(oldOrder.OrderID);

                string sqlDetail = @"
                    INSERT INTO Sales.OrderDetails
                               ([OrderID]
                               ,[ProductID]
                               ,[UnitPrice]
                               ,[Qty]
                               ,[Discount])
                         VALUES
                               (@OrderID
                               ,@ProductID
                               ,@UnitPrice
                               ,@Qty
                               , 0)
                    ";
                foreach (var detail in oldOrder.Details)
                {
                    if (detail.ProductID == 0)
                        continue;
                    SqlCommand cmdDetail = new SqlCommand(sqlDetail, conn);
                    cmdDetail.Parameters.Add(new SqlParameter("@OrderID", orderId));
                    cmdDetail.Parameters.Add(new SqlParameter("@ProductID", detail.ProductID));
                    cmdDetail.Parameters.Add(new SqlParameter("@UnitPrice", detail.UnitPrice));
                    cmdDetail.Parameters.Add(new SqlParameter("@Qty", detail.Qty));
                    cmdDetail.ExecuteNonQuery();
                }

                scope.Complete();
                conn.Close();
            }
        }

        /// <summary>
        /// 刪除訂單
        /// </summary>
        /// <param name="orderId"></param>
        public void DeleteOrder(int orderId)
        {
            SqlConnection conn = GetSqlConnection();

            string sql = @"
                    DELETE FROM  [Sales].[OrderDetails]
                    WHERE OrderID = @OrderID
                    DELETE FROM  [Sales].[Orders]
                    WHERE OrderID = @OrderID
                ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@OrderID", orderId));

            conn.Open();
            // 開啟交易控管
            SqlTransaction transaction = conn.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                cmd.ExecuteNonQuery();
                // 全部動作做完後執行Commit
                transaction.Commit();
            }
            catch (Exception)
            {
                // 有出問題則將此交易內的所有更動的資料Rollback
                transaction.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}