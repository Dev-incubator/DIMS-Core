using System;
using System.Collections.Generic;

namespace DIMS_Core.DataAccessLayer.Entities
{
    public partial class TaskTrack
    {
        public int TaskTrackId { get; set; }
        public int UserTaskId { get; set; }
        public DateTime TrackDate { get; set; }
        public string? TrackNote { get; set; }

        public virtual UserTask UserTask { get; set; }
    }
}
