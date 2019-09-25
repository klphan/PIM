using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM.Core
{
    public class Group : BaseEntity
    {
      
        // do we have to specify
        // public Guid 
        
        //navigate to Employee table
        //public Guid GroupLeader_ID { get; set; }
        public virtual Employee GroupLeader { get; set; }

        // Navigation property for Project
       
        public Project Project { get; set; }
        public byte[] Version { get; set; }
    }
}
