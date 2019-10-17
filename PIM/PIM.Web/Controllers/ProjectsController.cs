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

namespace PIM.Web.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects
        private GroupService groupService;
        private ProjectService projectService;
        private IEnumerable<Group> defaultGroupIdsLeaders;

        public ProjectsController()
        {
            projectService = new ProjectService();
            groupService = new GroupService();
            // defaultGroupIds to avoid repetition calling groupId() and re-initialise viewModel
            defaultGroupIdsLeaders = groupService.GetGroup();
        }

        //[Route("EditProjectForm/{id}") ]
        public ViewResult ProjectDetails(Guid? id)
        {
            if (!id.HasValue)
            {
                var defaultViewModel = new ProjectFormViewModel
                {
                    Groups = defaultGroupIdsLeaders
                };
                return View("ProjectDetails", defaultViewModel);
            }
            //Add a method to ProjectService to get project
            else
            {
                var project = projectService.GetProject(id.Value);
                List<string> empVisas = projectService.GetEmployees(id.Value);
                String memberVisas = String.Join(", ", empVisas.ToArray());
                var viewModel = new ProjectFormViewModel
                {

                    Project = project,
                    EditMode = true,
                    Groups = defaultGroupIdsLeaders,
                    // TODO: Get the list of ex employees in form of string
                    Members = memberVisas
                };
                return View("ProjectDetails", viewModel);
            }
        }
        
        public ActionResult CatchException(ProjectFormViewModel viewModel)
        {
            if (!viewModel.EditMode) {
                ModelState.Remove("Project.ID");
            }

            var errViewModel = new ProjectFormViewModel
            {
                //groupIds of viewModel is currently empty so must set back to default
                Groups = defaultGroupIdsLeaders,
                Project = viewModel.Project,
                Members = viewModel.Members,
                EditMode = viewModel.EditMode
            };
            //Step1: Check for data annotation attribute client side validation
            if (!ModelState.IsValid)
            {
                return View("ProjectDetails", errViewModel);
            }
            List<string> members = ParseEmployees(viewModel);
            try
            {
                if (viewModel.EditMode) {
                    projectService.Update(viewModel.Project, members);
                }
                else
                {
                    projectService.Create(viewModel.Project, members);
                }
               
            }
            //Step2: Check for business logic, custom validation class from the server
            catch (BusinessException ex)
            {
                //the assigment errorViewModel.Exception = ex; will prevent the printing 
                // of errot message: Please enter all the mandatory fields
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
        [HttpPost]
        public ActionResult Create(ProjectFormViewModel viewModel)
        {
            return CatchException(viewModel);//missing return cause bugs


            //var errViewModel = new ProjectFormViewModel
            //{
            //    //groupIds of viewModel is currently empty so must set back to default
            //    Groups = defaultGroupIdsLeaders,
            //    Project = viewModel.Project,
            //    Members = viewModel.Members,
            //};

            //ModelState.Remove("Project.ID"); //ModelState here is the ProjectFormViewModel
            //if (!ModelState.IsValid) //because the ModelState will check for empty ID also
            //{
            //    return View("ProjectDetails", errViewModel);
            //}
            //List<string> members = ParseEmployees(viewModel);
            //// use model state property to change the flow of the program
            ////, if the information entered is not valid, redirect the user to the same view
            ////Step2: Check for business logic, custom validation class from the server
            //try
            //{
            //    projectService.Create(viewModel.Project, members);
            //}
            //catch (InvalidProjectNumberException ex)
            //{
            //    //the assigment errorViewModel.Exception = ex; will prevent the printing 
            //    // of errot message: Please enter all the mandatory fields
            //    errViewModel.Exception = ex;
            //    ModelState.AddModelError(string.Empty, ex.Message);
            //    return View("ProjectDetails", errViewModel);
            //}
            //catch (InvalidEndDateException ex)
            //{
            //    errViewModel.Exception = ex;
            //    ModelState.AddModelError(string.Empty, ex.Message);
            //    return View("ProjectDetails", errViewModel);
            //}
            //catch (InvalidVisaException ex)
            //{
            //    errViewModel.Exception = ex;
            //    ModelState.AddModelError(string.Empty, ex.Message);
            //    return View("ProjectDetails", errViewModel);
            //}
            //return RedirectToAction("Index", "Projects");
        }

        private static List<string> ParseEmployees(ProjectFormViewModel viewModel)
        {
            List<string> members = new List<string>();
            if (viewModel.Members != null)
            {
                String[] separators = { ", ", ",", " ,", " " };
                members = viewModel.Members.Split(separators, StringSplitOptions.None).ToList();
            }

            return members;
        }

        [HttpPost]
        public ActionResult Update(ProjectFormViewModel viewModel)
        {
            // must turn the EditMode on before clicking EDIT button, otherwise will be 
            // treated as create mode
            viewModel.EditMode = true;
            return CatchException(viewModel);
            
            //List<string> members = ParseEmployees(viewModel);
            //var errViewModel = new ProjectFormViewModel
            //{
            //    //groupIds of viewModel is currently empty so must set back to default
            //    Groups = defaultGroupIdsLeaders,
            //    Project = viewModel.Project,
            //    Members = viewModel.Members,
            //};
            //try
            //{
            //    projectService.Update(viewModel.Project, members);
            //}
            //catch (BusinessException ex)
            //{
            //    errViewModel.Exception = ex;
            //    errViewModel.EditMode = true;
            //    ModelState.AddModelError(string.Empty, ex.Message);
            //    return View("ProjectDetails", errViewModel);
            //}

        }
        //TODO: Implement Search method
        public ActionResult Search(IndexPageViewModel viewModel)
        {
            IEnumerable<Project> projects = projectService.Search(viewModel.ProjectCriteria);
            var searchView = new IndexPageViewModel
            {
                ProjectCriteria = viewModel.ProjectCriteria,
                Projects = projects
            };

            return View("Index", searchView);
        }
        public ViewResult Index()
        {
            var projects = projectService.Index();

            var newIndexPageView = new IndexPageViewModel
            {
                Projects = projects,
                ProjectCriteria = new ProjectCriteria()

            };
            return View(newIndexPageView);
        }
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            projectService.Delete(id);
            return RedirectToAction("Index", "Projects");
        }

    }
}