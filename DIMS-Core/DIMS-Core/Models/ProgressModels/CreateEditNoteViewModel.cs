using System;

namespace DIMS_Core.Models.ProgressModels
{
    public class CreateEditNoteViewModel
    {
        public int UserTaskId { get; set; }
        public DateTime TrackDate { get; set; }
        public string TrackNote { get; set; }
        public string TaskName { get; set; }
    }
}