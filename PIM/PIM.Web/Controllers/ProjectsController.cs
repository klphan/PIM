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

namespace PIM.Web.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects
        private PIMContext _context;
        private GroupService groupService;
        private ProjectService projectService;
        private IEnumerable<Guid> defaultGroupIds;

        public ProjectsController()
        {
            _context = new PIMContext();
            projectService = new ProjectService();
            groupService = new GroupService();
            // defaultGroupIds to avoid repetition calling groupId() and re-initialise viewModel
            defaultGroupIds = groupService.GetGroupId();
        }
        public ViewResult NewProjectForm()
        {
            //TODO: Build GroupService to get all group
            var defaultViewModel = new ProjectFormViewModel
            {
                GroupIds = defaultGroupIds
            };
            return View("ProjectForm", defaultViewModel);
        }
        //[Route("EditProjectForm/{id}") ]
        public ViewResult EditProjectForm(Guid id)
        {
            //Add a method to ProjectService to get project
            var project = projectService.GetProject(id);
            var viewModel = new ProjectFormViewModel
            {

                Project = project,
                EditMode = true,
                GroupIds = defaultGroupIds
                // TODO: Get the list of ex employees in form of string
                //Members = 
            };
            return View("ProjectForm", viewModel);
        }
        [HttpPost]
        public ActionResult Create(ProjectFormViewModel viewModel)
        {
            //BUGS always enter if statement regardless: fix bug by remove the check for Project.ID in view Model
            ModelState.Remove("Project.ID");
            if (!ModelState.IsValid) //because the ModelState will check for empty ID also
            {
                var errViewModel = new ProjectFormViewModel
                {
                    //groupIds of viewModel is currently empty so must set back to default
                    GroupIds = defaultGroupIds,
                    Project = viewModel.Project,
                    Members = viewModel.Members,
                };
                return View("ProjectForm", errViewModel);
            }
            
            //convert the string Memebers into a list of members
            String[] separators = { ", ", ",", " ,", " " };
            List<string> members = viewModel.Members.Split(separators, StringSplitOptions.None).ToList();
            // use model state property to change the flow of the program
            //, if the information entered is not valid, redirect the user to the same view
            
        
            projectService.Create(viewModel.Project, members);

            //TODO: empty string in members-> the following visa does not exist
            //TODO: dropdownList for Group, with group leader name

            return RedirectToAction("Index", "Projects");
        }
        [HttpPost]
        public ActionResult Update(ProjectFormViewModel viewModel)
        {
            String[] separators = { ", ", ",", " ,", " " };
            List<string> members = viewModel.Members.Split(separators, StringSplitOptions.None).ToList();

            projectService.Update(viewModel.Project, members);
            return RedirectToAction("Index", "Projects");
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
            var projects = _context.Projects.OrderBy(p => p.ProjectNumber).ToList();

            var newIndexPageView = new IndexPageViewModel
            {
                Projects = projects,
                ProjectCriteria = new ProjectCriteria()

            };
            return View(newIndexPageView);
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //public ActionResult Delete(Guid id)
        //{
        //    projectService.Delete(id);
        //}

    }
}