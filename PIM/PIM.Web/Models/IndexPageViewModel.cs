﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIM.Core;

namespace PIM.Web.Models
{
    public class IndexPageViewModel
    {
        public IEnumerable<Project> Projects { get; set; }
        public ProjectCriteria ProjectCriteria { get; set; }
    }
}