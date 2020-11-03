using DIMS_Core.BusinessLayer.Models.UserTask;
using System;
using System.Collections.Generic;

namespace DIMS_Core.BusinessLayer.Models.Task
{
    public class TaskModel
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }

        public ICollection<UserTaskModel> UserTask { get; set; }
    }
}
