using System;

namespace DIMS_Core.Models.ProgressModels
{
    public class UserTaskViewModel
    {
        public int UserTaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public string State { get; set; }
    }
}