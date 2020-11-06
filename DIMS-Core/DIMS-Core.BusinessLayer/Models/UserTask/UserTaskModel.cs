using DIMS_Core.BusinessLayer.Models.Members;
using TaskEntity = DIMS_Core.DataAccessLayer.Entities.Task;

namespace DIMS_Core.BusinessLayer.Models.UserTask
{
    public class UserTaskModel
    {
        public int UserTaskId { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int StateId { get; set; }

        public UserProfileModel User { get; set; }
        public TaskEntity Task { get; set; }
    }
}
