﻿using System;
using System.Collections.Generic;

namespace DIMS_Core.Models
{
    public partial class VUserProgress
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int TaskTrackId { get; set; }
        public string UserName { get; set; }
        public string TaskName { get; set; }
        public string TrackNote { get; set; }
        public string TrackDate { get; set; }
    }
}
