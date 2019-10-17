using PIM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Infrastructure.Services
{
    public class GroupService
    {
        public IEnumerable<Group> GetGroup()
        {
            using (var unitOfWork = new UnitOfWork(new PIMContext()))
            {
                
                var allGroups = unitOfWork.Group.Get().ToList();
                List<string> groupLeaderNames = new List<string>();
                foreach (Group group in allGroups)
                {
                    groupLeaderNames.Add(group.GroupLeader.FirstName + group.GroupLeader.LastName);
                };
                return allGroups;
            }
        }
    }
}
