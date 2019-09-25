using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PIM.Core
{

    public class Project : BaseEntity
    {
        public Guid Group_ID;

        public decimal ProjectNumber { get; set; }

        public string Name { get; set; }

        public string Customer { get; set; }

        public Status Status { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
      
        public byte[] Version { get; set; }

        // navigation property to Group dbo
        //public Guid Group_ID { get; set; }
        public virtual Group Group { get; set; }
        //navigation property for ProjectEmployee dbo

        public virtual ProjectEmployee ProjectEmployee { get; set; }

        
       
        // because you already have the ProjectEmployee table created seperately
        // Only need to declare single navigation property and not a collection property
        // for the DB to self create relational table
        //    private ICollection<Employee> _employees;
        //    public virtual ICollection<Employee> Employees => _employees ?? (_employees = new List<Employee>());
    }

    
}
