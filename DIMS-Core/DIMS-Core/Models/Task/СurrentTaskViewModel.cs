using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DIMS_Core.Models.Task
{
    public class CurrentTaskViewModel
    {
        public int TaskId { get; set; }
        public int UserTaskId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public string Status { get; set; }
    }
}
