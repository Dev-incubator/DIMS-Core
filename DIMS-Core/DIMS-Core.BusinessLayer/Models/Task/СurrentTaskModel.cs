using System;

namespace DIMS_Core.BusinessLayer.Models.Task
{
    public class CurrentTaskModel
    {
        public int TaskId { get; set; }
        public int UserTaskId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public string Status { get; set; }
    }
}
