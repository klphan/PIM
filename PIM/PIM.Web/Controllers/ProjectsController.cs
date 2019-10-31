using PIM.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PIM.Infrastructure.Services;
using PIM.Core.Exceptions;
using PagedList;
using System.Threading;
using System.Globalization;
using PIM.Core.Entities;

namespace PIM.Web.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects
        private GroupService _groupService;
        private ProjectService _projectService;
        private EmployeeService _employeeService;


        public ProjectsController()
        {
            _projectService = new ProjectService();
            _groupService = new GroupService();
            _employeeService = new EmployeeService();
        }


        public ActionResult Index(ProjectCriteria projectCriteria)
        {
            

            IPagedList<Project> projects = _projectService.Search(projectCriteria);
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
                    Groups = _groupService.GetGroup(),
                    EmployeesList = GetAllEmployeesNames(_employeeService.GetEmployee())
                };
                return View("ProjectDetails", defaultViewModel);
            }
            else
            {
                var project = _projectService.GetProjectWithEmployees(id.Value);
                IEnumerable<string> visas = DisplayMembers(project);
                var viewModel = new ProjectFormViewModel
                {
                    Project = project,
                    EditMode = true,
                    Groups = _groupService.GetGroup(),
                    EmployeesList = GetAllEmployeesNames(_employeeService.GetEmployee()),
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
            _projectService.Delete(id);
            return RedirectToAction("Index", "Projects");
        }

        [HttpPost]
        public void DeleteRange(List<Guid> projectIds)
        {
            _projectService.DeleteRange(projectIds);
        }

        private ActionResult CatchException(ProjectFormViewModel viewModel)
        {
            if (!viewModel.EditMode)
            {
                ModelState.Remove("Project.ID");
            }

            var errViewModel = new ProjectFormViewModel
            {
                Groups = _groupService.GetGroup(),
                Project = viewModel.Project,
                Members = viewModel.Members,
                EditMode = viewModel.EditMode,
                EmployeesList = GetAllEmployeesNames(_employeeService.GetEmployee())
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
                    _projectService.Update(viewModel.Project, viewModel.Members);
                }
                else
                {
                    _projectService.Create(viewModel.Project, viewModel.Members);
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

        private static List<SelectListItem> GetAllEmployeesNames(IEnumerable<Employee> employeesFromDb)
        {
            List<SelectListItem> employeesList = new List<SelectListItem>();
            foreach (var emp in employeesFromDb)
            {
                employeesList.Add(new SelectListItem
                    {Text = emp.Visa + @": " + emp.LastName + @" " + emp.FirstName, Value = emp.Visa});
            }
            return employeesList;
        }
    }
}