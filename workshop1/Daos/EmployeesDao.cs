using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using workshop1.Models;

namespace workshop1.Daos
{
    public class EmployeesDao : BaseDao
    {
        public List<Employee> GetAllEmployees()
        {
            List<Employee> result = new List<Employee>();
            using (SqlConnection conn = GetSqlConnection())
            {

                string sql = "select  * from HR.Employees";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                DataSet ds = new DataSet();
                adapter.Fill(ds);


                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(new Employee
                    {
                        EmployeeID = int.Parse(row["EmployeeID"].ToString()),
                        LastName = row["LastName"].ToString(),
                        FirstName = row["FirstName"].ToString()
                    });
                }
            }
            return result;
        }
    }
}