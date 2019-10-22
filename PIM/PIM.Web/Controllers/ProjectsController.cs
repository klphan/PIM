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
        private IEnumerable<Group> defaultGroupIdsLeaders;

        public ProjectsController()
        {
            projectService = new ProjectService();
            groupService = new GroupService();
            defaultGroupIdsLeaders = groupService.GetGroup();
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
                    Groups = defaultGroupIdsLeaders
                };
                return View("ProjectDetails", defaultViewModel);
            }
            else
            {
                var project = projectService.GetProjectWithEmployees(id.Value);
                String memberVisas = DisplayMembers(project);
                var viewModel = new ProjectFormViewModel
                {
                    Project = project,
                    EditMode = true,
                    Groups = defaultGroupIdsLeaders,
                    Members = memberVisas
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
        public ActionResult DeleteRange(IEnumerable<Guid> projectIdsToDelete)
        {//name of parameter matching the name of checkbox in index.cshtml
            // because the submit delete button will pass the list of selected id into 
            // this argument by default
            projectService.DeleteRange(projectIdsToDelete);
            return RedirectToAction("Index", "Projects");
        }
        private ActionResult CatchException(ProjectFormViewModel viewModel)
        {
            if (!viewModel.EditMode)
            {
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
                if (viewModel.EditMode)
                {
                    projectService.Update(viewModel.Project, members);
                }
                else
                {
                    projectService.Create(viewModel.Project, members);
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
        private static String DisplayMembers(Project project)
        {
            List<string> visas = new List<string>();
            foreach (ProjectEmployee pe in project.ProjectEmployees)
            {
                visas.Add(pe.Employee.Visa);
            }
            return String.Join(", ", visas.ToArray());
        }

    }
}