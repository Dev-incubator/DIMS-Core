using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DIMS_Core.DataAccessLayer.Entities
{
    public partial class VTask
    {
        [Key]
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
    }
}
