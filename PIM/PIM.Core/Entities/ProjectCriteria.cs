namespace PIM.Core.Entities
{
    public class ProjectCriteria
    {
        public string Text { get; set; }

        public Status? Status { get; set; }

        public int Page { get; set; } = 1;

        public int ItemsPerPage { get; set; } = 5;

        public string SortProperty { get; set; }

        public SortDirection? SortDirection { get; set; }
    }
}
