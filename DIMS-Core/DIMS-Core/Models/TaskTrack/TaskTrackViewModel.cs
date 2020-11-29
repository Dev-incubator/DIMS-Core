using System;
using System.ComponentModel.DataAnnotations;

namespace DIMS_Core.Models.TaskTrack
{
    public class TaskTrackViewModel
    {
        public int TaskTrackId { get; set; }
        public int UserTaskId { get; set; }
        [Required(ErrorMessage = "Track date is required")]
        public DateTime TrackDate { get; set; }
        [Required(ErrorMessage = "Track note is required")]
        public string TrackNote { get; set; }
    }
}
