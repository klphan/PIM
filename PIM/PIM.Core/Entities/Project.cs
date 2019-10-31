using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PIM.Core.Entities
{

    public class Project : BaseEntity
    {
        public Guid GroupId { get; set; }

        [Required]
        public int ProjectNumber { get; set; }
        
        //Fluent API property setting is not recognised by mvc so have to set data annotation manually
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Customer { get; set; }

        [Required]
        public Status Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime StartDate { get; set; }
       
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
      
        [Timestamp]
        public byte[] Version { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; }
        public Project()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            // this virtual method is pure function that returns some value and does not depend on the state of the derived type
            ProjectEmployees = new Collection<ProjectEmployee>();
        }
    }
}
