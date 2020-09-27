using System;
using System.Collections.Generic;

namespace DIMS_Core.Models
{
    public partial class VUserTask
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string DeadlineDate { get; set; }
        public string State { get; set; }
    }
}
