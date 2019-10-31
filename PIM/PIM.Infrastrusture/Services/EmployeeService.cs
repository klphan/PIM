using System.Collections.Generic;
using System.Linq;
using PIM.Core.Entities;

namespace PIM.Infrastructure.Services
{
    public class EmployeeService
    {
        public IEnumerable<Employee> GetEmployee()
        {
            using (var unitOfWork = new UnitOfWork(new PimContext()))
            {
                var allEmployees = unitOfWork.Employee.Get().ToList();
                return allEmployees;
            }
        }
    }
}
