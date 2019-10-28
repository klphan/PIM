using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace PIM.Core
{

    public class Employee : BaseEntity
    {
        public string Visa { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDay { get; set; }
        [Timestamp]
        public byte[] Version { get; set; }

        //navigation property for Group
        public virtual Group IsGroupLeader { get; set; }

        //Navigation for ProjectEmployee
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; }
        public Employee()
        {
            ProjectEmployees = new Collection<ProjectEmployee>();
        }


    }
}