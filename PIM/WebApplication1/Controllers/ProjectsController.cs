
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIM.Core;
using PIM.Infrastructure;
using PIM.Infrastructure.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects

        // TODO: View: New Project



        private PIMContext _context;
          

       // New Project Form
        public ViewResult New()
        {
            // Dropdown List Group
            var groups = _context.Groups.ToList();
            var viewModel = new NewProjectViewModel
            {
                Groups = groups,

            };
            return View();
            // return Content("Hello World");
        }
        public ActionResult Random()
        {
            return Content("Hello World");
        }

        // TODO: View: Edit Project

        public ActionResult Edit(int movieId)
        {
            return Content("id=" + movieId);
        }

        // TODO: View: Search results for project
        public ActionResult Index(ProjectCriteria c)
        {
            ProjectService service = new ProjectService(); 
            return View(service.Search(c));
        }

        

    }
}