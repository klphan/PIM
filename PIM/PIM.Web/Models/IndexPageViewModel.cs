using PIM.Core.Entities;
using PagedList;

namespace PIM.Web.Models
{
    public class IndexPageViewModel
    {
        public IPagedList<Project> Projects { get; set; }
        public ProjectCriteria ProjectCriteria { get; set; }
    }
}