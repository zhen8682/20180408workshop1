using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using workshop1.Daos;

namespace workshop1.Models.Services
{
    public class EmployeeService
    {

        /// <summary>
        /// 取得所有員工
        /// </summary>
        /// <returns></returns>
        public IList<Employee> GetEmployees()
        {
            EmployeesDao dao = new EmployeesDao();
            return dao.GetAllEmployees();
        }
    }
}