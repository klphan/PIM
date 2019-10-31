using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization.Formatters;

namespace PIM.Core.Entities
{
    public class Group : BaseEntity
    {
        // if it is a virtual property i.e navigation property
        /// <summary>
        /// if it is a virtual property i.e navigation property and
        /// you want to use it when you refer to Group i.e Group.GroupLeader
        /// then when you get the Group from the DB you must have Include(g=>g.GroupLeader)
        /// </summary>
        public virtual Employee GroupLeader { get; set; }
        // Navigation property for Project
        public Project Project { get; set; }
        [Timestamp]
        public byte[] Version { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
