using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace workshop1.Daos
{
    public abstract class BaseDao
    {
        /// <summary>
        /// 連線字串
        /// </summary>
        protected string ConnString;

        /// <summary>
        /// 建構子 取得連線字串
        /// </summary>
        protected BaseDao()
        {
            ConnString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        }

        /// <summary>
        /// 取得SqlConnection
        /// </summary>
        /// <returns></returns>
        protected SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnString);
        }        
    }
}
