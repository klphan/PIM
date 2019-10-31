using PIM.Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PIM.Core.Entities;

namespace PIM.Infrastructure.Services
{
    public class GroupService
    {
        public IEnumerable<Group> GetGroup()
        {
            using (var unitOfWork = new UnitOfWork(new PimContext()))
            {
                var allGroups = unitOfWork.Group.Get().Include(g => g.GroupLeader).ToList();
                return allGroups;
            }
        }
    }
}
