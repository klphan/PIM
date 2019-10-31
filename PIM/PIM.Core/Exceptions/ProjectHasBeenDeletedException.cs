namespace PIM.Core.Exceptions
{
    public class ProjectHasBeenDeletedException : BusinessException
    {
        public ProjectHasBeenDeletedException(string message) : base(message)
        {
        }
    }
}
