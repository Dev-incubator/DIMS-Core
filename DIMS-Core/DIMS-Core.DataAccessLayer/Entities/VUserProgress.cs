using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DIMS_Core.DataAccessLayer.Entities
{
    public partial class VUserProgress
    {
        [Key]
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int TaskTrackId { get; set; }
        public string UserName { get; set; }
        public string TaskName { get; set; }
        public string TrackNote { get; set; }
        public DateTime TrackDate { get; set; }
    }
}
