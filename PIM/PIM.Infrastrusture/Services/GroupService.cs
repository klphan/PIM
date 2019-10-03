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
        public IEnumerable<Guid> GetGroupId()
        {
            using (var unitOfWork = new UnitOfWork(new PIMContext()))
            {
                
                var allGroups = unitOfWork.Group.Get().ToList();
                List<Guid> GroupIds= new List<Guid>();

                foreach (var group in allGroups)
                {
                    GroupIds.Add(group.ID);
                }
                return GroupIds;
            }
        }
    }
}
