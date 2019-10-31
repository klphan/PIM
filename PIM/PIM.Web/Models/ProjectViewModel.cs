using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PIM.Core.Entities;

namespace PIM.Web.Models
{
    public class ProjectFormViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        // List of employees for dropdown list
        public IEnumerable<SelectListItem> EmployeesList { get; set; }
        public Group Group { get; set; }
        public Project Project { get; set; }
        public IEnumerable<string> Members { get; set; }
        public bool EditMode { get; set; }
        public Exception Exception { get; set; }
    }
}