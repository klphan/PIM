using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PIM.Core
{

    public class Project : BaseEntity
    {
        public Guid GroupId { get; set; }
        // beacause this conflicts the navagation property Group with the method mapkey
        public decimal ProjectNumber { get; set; }

        public string Name { get; set; }

        public string Customer { get; set; }

        public Status Status { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
      
        public byte[] Version { get; set; }

        // navigation property to Group dbo
        public virtual Group Group { get; set; }
        //navigation property for ProjectEmployee dbo

        public virtual ProjectEmployee ProjectEmployee { get; set; }

    }

    
}
