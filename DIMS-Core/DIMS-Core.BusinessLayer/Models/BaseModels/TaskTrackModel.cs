using System;

namespace DIMS_Core.BusinessLayer.Models.BaseModels
{
    public class TaskTrackModel : BaseDTOModel
    {
        public int? TaskTrackId { get; set; }
        public int UserTaskId { get; set; }
        public DateTime TrackDate { get; set; }
        public string TrackNote { get; set; }

        protected internal override int PKId => TaskTrackId.Value;
    }
}