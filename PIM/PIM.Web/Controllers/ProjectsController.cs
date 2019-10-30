using PIM.Infrastructure;
using PIM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIM.Core;
using System.Data.Entity;
using PIM.Infrastructure.Services;
using PIM.Core.Exceptions;
using PagedList;
using PagedList.Mvc;

namespace PIM.Web.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects
        private GroupService groupService;
        private ProjectService projectService;
        private EmployeeService employeeService;

        public ProjectsController()
        {
            projectService = new ProjectService();
            groupService = new GroupService();
            employeeService = new EmployeeService();
        }

        public ActionResult Index(ProjectCriteria projectCriteria)
        {
            IPagedList<Project> projects = projectService.Search(projectCriteria);
            var searchView = new IndexPageViewModel
            {
                ProjectCriteria = projectCriteria,
                Projects = projects
            };
            return View(searchView);
        }

        public ViewResult ProjectDetails(Guid? id)
        {
            if (!id.HasValue)
            {
                var defaultViewModel = new ProjectFormViewModel
                {
                    Groups = groupService.GetGroup(),
                    EmployeesList = GetAllEmployeesNames(employeeService.GetEmployee())

                };
                return View("ProjectDetails", defaultViewModel);
            }
            else
            {
                var project = projectService.GetProjectWithEmployees(id.Value);
                IEnumerable<string> visas = DisplayMembers(project);
                var viewModel = new ProjectFormViewModel
                {
                    Project = project,
                    EditMode = true,
                    Groups = groupService.GetGroup(),
                    EmployeesList = GetAllEmployeesNames(employeeService.GetEmployee()),
                    Members = visas
                    
                };
                return View("ProjectDetails", viewModel);
            }
        }
        [HttpPost]
        public ActionResult Create(ProjectFormViewModel viewModel)
        {
            return CatchException(viewModel);
        }
        [HttpPost]
        public ActionResult Update(ProjectFormViewModel viewModel)
        {
            viewModel.EditMode = true;
            return CatchException(viewModel);
        }
     
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            projectService.Delete(id);
            return RedirectToAction("Index", "Projects");
        }

        [HttpPost]
        public void DeleteRange(List<Guid> projectIds)
        {
            projectService.DeleteRange(projectIds);
        }
        private ActionResult CatchException(ProjectFormViewModel viewModel)
        {
            if (!viewModel.EditMode)
            {
                ModelState.Remove("Project.ID");
            }

            var errViewModel = new ProjectFormViewModel
            {
                Groups = groupService.GetGroup(),
                Project = viewModel.Project,
                Members = viewModel.Members,
                EditMode = viewModel.EditMode,
                EmployeesList = GetAllEmployeesNames(employeeService.GetEmployee())
            };
            //Step1: Check for data annotation attribute client side validation
            if (!ModelState.IsValid)
            {
                return View("ProjectDetails", errViewModel);
            }
            try
            {
                if (viewModel.EditMode)
                {
                    projectService.Update(viewModel.Project, viewModel.Members);
                }
                else
                {
                    projectService.Create(viewModel.Project, viewModel.Members);
                }
            }
            catch (BusinessException ex)
            {
                errViewModel.Exception = ex;
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("ProjectDetails", errViewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                errViewModel.Exception = ex;
                return View("ProjectDetails", errViewModel);
            }
            return RedirectToAction("Index", "Projects");
        }
      
        private IEnumerable<string> DisplayMembers(Project project)
        {
            List<string> visas = new List<string>();
            foreach (ProjectEmployee pe in project.ProjectEmployees)
            {
                visas.Add(pe.Employee.Visa);
            }
            return visas;
        }
        private List<SelectListItem> GetAllEmployeesNames(IEnumerable<Employee> employeesFromDB)
        {
            List<SelectListItem> employeesList = new List<SelectListItem>();
            foreach (var emp in employeesFromDB)
            {
                employeesList.Add(new SelectListItem 
                { Text = emp.Visa + ": " + emp.LastName + " " + emp.FirstName, Value = emp.Visa });
            }
            return employeesList;
        }
    }
}