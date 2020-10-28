namespace DIMS_Core.DataAccessLayer.Filters
{
    public class UserTaskFilter : BaseFilter
    {
        public int? TaskId { get; set; }

        public UserTaskFilter() { }

        public UserTaskFilter(int TaskId)
        {
            this.TaskId = TaskId;
        }
    }
}