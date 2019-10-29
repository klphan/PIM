using PIM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PIM.Web.Models
{
    public class ProjectFormViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        // List of employees for dropdown list
        public IEnumerable<SelectListItem> EmployeesList { get; set; }
        public Group Group { get; set; }
        public Project Project { get; set; }
        //TODO: Add custom validation for Members
        public IEnumerable<string> Members { get; set; }
        public bool EditMode { get; set; }
        public Exception Exception { get; set; }
    }
}