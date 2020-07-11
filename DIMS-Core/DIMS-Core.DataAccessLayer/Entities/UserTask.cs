using System;
using System.Collections.Generic;

namespace DIMS_Core.DataAccessLayer.Entities
{
    public partial class UserTask
    {
        public UserTask()
        {
            TaskTrack = new HashSet<TaskTrack>();
        }

        public int UserTaskId { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int? StateId { get; set; }

        public virtual TaskState State { get; set; }
        public virtual Task Task { get; set; }
        public virtual UserProfile User { get; set; }
        public virtual ICollection<TaskTrack> TaskTrack { get; set; }
    }
}
