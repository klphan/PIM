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
        [Required]
        // because this conflicts the navagation property Group with the method mapkey
        [Display(Name = "Project Number *")]
        
        public int ProjectNumber { get; set; }
        
        [Display(Name = "Project Name *")]
        //Fluent API property setting is not recognised by mvc so have to set data annotation manually
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Customer *")]
        [Required]
        [StringLength(50)]
        public string Customer { get; set; }

        [Display(Name = "Status *")]
        [Required]
        public Status Status { get; set; }

        [Display(Name = "Start Date *")]
        [Required]
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
