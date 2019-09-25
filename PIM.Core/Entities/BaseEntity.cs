using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Core
{
   
   public class BaseEntity
    {
        public Guid ID { get; set; }
        public BaseEntity()
        {
            ID = Guid.NewGuid();
        }
    }
    
    
}
