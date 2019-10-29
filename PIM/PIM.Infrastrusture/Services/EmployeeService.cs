using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIM.Core;

namespace PIM.Infrastructure.Services
{
    public class EmployeeService
    {
        public IEnumerable<Employee> GetEmployee()
        {
            using (var unitOfWork = new UnitOfWork(new PIMContext()))
            {

                var allEmployees = unitOfWork.Employee.Get().ToList();
                return allEmployees;
            }
        }
    }
}
