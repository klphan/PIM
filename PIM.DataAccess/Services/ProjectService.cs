using PIM.Core;
using PIM.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Infrastructure.Services
{
    public class ProjectService
    {
        // create context for Validate and Create
        private void Validate(Project a, UnitOfWork unitOfWork, bool editMode)
        {
            if (editMode == false)
            {
                bool foundProject = unitOfWork.Project.Get().Any(p => p.ProjectNumber == a.ProjectNumber);

                if (foundProject)
                {
                    var message = "The project number already existed. Please select a different project number.";
                    Console.WriteLine(message);
                    throw new InvalidProjectNumberException(message);
                }
            }
            if (a.EndDate != null && a.EndDate < a.StartDate)
            {
                var message = "End date must be later than start date";
                Console.WriteLine(message);
                throw new InvalidEndDateException(message);
            }

        }

        private IList<Employee> ValidateMembers(IEnumerable<string> members, UnitOfWork unitOfWork)
        {
            List<string> invalidVisa = new List<string>();
            List<Employee> validVisa = new List<Employee>();

            foreach (string member in members)
            {
                var foundEmployee = unitOfWork.Employee.Get().FirstOrDefault(e => e.Visa == member);

                //(foundEmployee == null) ? validVisa.Add(foundEmployee) : invalidVisa.Add(member);

                if (foundEmployee == null)
                {
                    invalidVisa.Add(member);
                }
                else
                {
                    validVisa.Add(foundEmployee);
                }
            }

            if (invalidVisa.Count() > 0)
            {
                var message = "The following visas do not exist" + string.Join(",", invalidVisa);
                Console.WriteLine(message);
                throw new InvalidVisaException(message);
            }

            return validVisa;
        }
        public void Create(Project a, List<string> members)
        {
            using (var unitOfWork = new UnitOfWork(new PIMContext()))
            {
                Validate(a, unitOfWork, editMode: false);
                IList<Employee> validEmployees = ValidateMembers(members, unitOfWork);

                unitOfWork.Project.Add(a);
                // add record to EmployeeProject table
                foreach (Employee validEmployee in validEmployees)
                {
                    unitOfWork.ProjectEmployee.Add(
                        new ProjectEmployee
                        {
                            Project = a,
                            Employee = validEmployee
                        });
                }

                unitOfWork.Commit();
            }

        }
        public void Update(Project a, List<string> members)
        {
            using (var unitOfWork = new UnitOfWork(new PIMContext()))
            {
                Validate(a, unitOfWork, editMode: true);
                IList<Employee> validEmployees = ValidateMembers(members, unitOfWork);
                Project selected = unitOfWork.Project.FindById(a.ID);
                //Update record for the Project table
                selected.Customer = a.Customer;
                selected.Group_ID = a.Group_ID;
                selected.EndDate = a.EndDate;
                selected.StartDate = a.StartDate;
                selected.Status = a.Status;


                // ADD record to EmployeeProject table

                // return a list of projectEmployees in ProjectEmployee table that has the matching project number
                var exProjectEmployees = unitOfWork.ProjectEmployee.Get()
                    .Where(pe => pe.Project == selected)
                    .ToList();
                // if employee is not in the NEW members list, delete that employee record
                foreach (ProjectEmployee pe in exProjectEmployees)
                {

                    bool matched = validEmployees.Contains(pe.Employee);

                    if (!matched)
                    {
                        var exProjectEmployee = unitOfWork.ProjectEmployee.Get()
                            .FirstOrDefault(p => p.Employee == pe.Employee);
                        unitOfWork.ProjectEmployee.Remove(exProjectEmployee.ID);
                    }
                }
                // if member is not in the employees list, add new ProjectEmployee record
                // extract the employees from exprojectEmployees to list
                List<Employee> exEmployees = new List<Employee>();
                foreach (ProjectEmployee pe in exProjectEmployees)
                {
                    exEmployees.Add(pe.Employee);
                }

                foreach (Employee validEmployee in validEmployees)
                {
                    bool duplicated = exEmployees.Contains(validEmployee);

                    if (!duplicated)
                    {
                        unitOfWork.ProjectEmployee.Add(
                        new ProjectEmployee
                        {
                            Project = a,
                            Employee = validEmployee
                        });
                    }
                }
                unitOfWork.Commit();
            }
        }

        public IEnumerable<Project> Search(ProjectCriteria a)
        {
            using (var unitOfWork = new UnitOfWork(new PIMContext()))
            {
                // if the full criteria is filled
                if (a.Text.Length > 0 && a.Status != null)
                {
                    // build a query with matching status
                    var result =
                        unitOfWork.Project.Get()
                        .Where(p => p.Status == a.Status)
                        .Where(p => p.Name.Contains(a.Text)
                                || p.Customer.Contains(a.Text)
                                || p.ProjectNumber.ToString().Contains(a.Text))
                        .ToList();
                    return result;
                }
                // only status is selected
                if (a.Text == "" && a.Status != null)
                {
                    var result = unitOfWork.Project.Get()
                        .Where(p => p.Status == a.Status)
                        .ToList();
                    return result;
                }

                // only text is filled

                if (a.Text.Length > 0 && a.Status == null)
                {
                    var result = unitOfWork.Project.Get()
                        .Where(p => p.Name.Contains(a.Text)
                                || p.Customer.Contains(a.Text)
                                || p.ProjectNumber.ToString().Contains(a.Text))
                        .ToList();
                    return result;
                }

                // empty criteria
                else
                {
                    return unitOfWork.Project.Get().ToList();
                }
            }

        }

        public void Delete(Project a)
        {
            // build query to remove a project with an id
            using (var unitOfWork = new UnitOfWork(new PIMContext()))
            {
                // remove record from the project table
                unitOfWork.Project.Remove(a.ID);

                // remove associated ProjectEmployee Record

                var filtered = unitOfWork.ProjectEmployee.Get().Where(pe => pe.Project == a).ToList();
                foreach (ProjectEmployee exProjectEmployee in filtered)
                {
                    unitOfWork.ProjectEmployee.Remove(exProjectEmployee.ID);
                }
                unitOfWork.Commit();
            }

        }

    }
}

