using System;
using System.Collections.Generic;

namespace DIMS_Core.DataAccessLayer.Entities
{
    public partial class VUserTrack
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int TaskTrackId { get; set; }
        public string TaskName { get; set; }
        public string TrackNote { get; set; }
        public DateTime? TrackDate { get; set; }
    }
}
