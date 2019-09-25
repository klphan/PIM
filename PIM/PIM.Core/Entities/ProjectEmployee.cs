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

    }
}
