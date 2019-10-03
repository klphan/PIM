using PIM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebApplication1.Models
{
    public class NewProjectViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        public Project Project { get; set; }

    }
}