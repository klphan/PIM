using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PIM.Core
{

    public class Project : BaseEntity
    {
       

        [Display(Name = "Group *")]
        public Guid GroupId { get; set; }

        // because this conflicts the navagation property Group with the method mapkey
        [Display(Name = "Project Number *")]
        public decimal ProjectNumber { get; set; }

        [Display(Name = "Project Name *")]
        public string Name { get; set; }
        [Display(Name = "Customer *")]
        public string Customer { get; set; }

        [Display(Name = "Status *")]
        public Status Status { get; set; }

        [Display(Name = "Start Date *")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
      
        public byte[] Version { get; set; }

        // navigation property to Group dbo
        public virtual Group Group { get; set; }
        //navigation property for ProjectEmployee dbo

        //public virtual ProjectEmployee ProjectEmployee { get; set; }
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; }

        public Project()
        {
            ProjectEmployees = new Collection<ProjectEmployee>();
        }

    }

    
}
