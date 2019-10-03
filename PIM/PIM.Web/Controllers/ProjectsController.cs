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

        public ProjectsController()
        {
            _context = new PIMContext();
            projectService = new ProjectService();
            groupService = new GroupService();
        }
        public ViewResult NewProjectForm()
        {
            //TODO: Build GroupService to get all group
            var groupIds = groupService.GetGroupId();
            var viewModel = new ProjectFormViewModel
            {
                GroupIds = groupIds
            };
            return View("ProjectForm", viewModel);
        }
        //[Route("Projects/EditProjectForm/{id}") ]
        public ViewResult EditProjectForm(Guid id)
        {
            //Add a method to ProjectService to get project
            var project = projectService.GetProject(id);
            var groupIds = groupService.GetGroupId();
            
            var viewModel = new ProjectFormViewModel
            {

                Project = project,
                EditMode = true,
                GroupIds = groupIds
                // TODO: Get the list of ex employees in form of string
                //Members = 
            };
            return View("ProjectForm", viewModel);
        }
        [HttpPost]
        public ActionResult Create(ProjectFormViewModel viewModel)
        {
            //convert the string Memebers into a list of members
            String[] separators = { ", ", ",", " ,", " " };
            List<string> members = viewModel.Members.Split(separators, StringSplitOptions.None).ToList();
            projectService.Create(viewModel.Project, members);

            //TODO: empty string in members-> the following visa does not exist
            //TODO: dropdownList for Group

            return RedirectToAction("Index", "Projects");
        }
        [HttpPost]
        public ActionResult Update(ProjectFormViewModel viewModel)
        {
            String[] separators = { ", ", ",", " ,", " " };
            List<string> members = viewModel.Members.Split(separators, StringSplitOptions.None).ToList();

            projectService.Update(viewModel.Project, members);
            return View("Index");
        }
        //TODO: Implement Search method
        public ActionResult Search(ProjectFormViewModel viewModel)
        {

            return View("Index");
        }
        public ViewResult Index()
        {
            var projects = _context.Projects.OrderBy(p => p.ProjectNumber).ToList();
            return View(projects);
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

    }
}