using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Core
{
    public class ProjectEmployee : BaseEntity
    {
        
        public virtual Project Project { get; set; }

        public virtual Employee Employee { get; set; }
        public Guid? ProjectId { get; set; }
        // Guid? is to make the foreign key nullable, for the 0:n relationship
        public Guid? EmployeeId { get; set; }
    }
}
