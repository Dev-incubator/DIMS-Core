using DIMS_Core.BusinessLayer.Models.Task;

namespace DIMS_Core.BusinessLayer.Models.UserTask
{
    public class UserTaskModel
    {
        public int UserTaskId { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int StateId { get; set; }

        public TaskModel Task { get; set; }
    }
}
