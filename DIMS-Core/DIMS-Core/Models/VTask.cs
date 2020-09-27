using System;
using System.Collections.Generic;

namespace DIMS_Core.Models
{
    public partial class VTask
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string DeadlineDate { get; set; }
    }
}
