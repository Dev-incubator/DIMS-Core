using System;

namespace DIMS_Core.BusinessLayer.Models.BaseModels
{
    public class VUserTrackModel : BaseDTOModel
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int TaskTrackId { get; set; }
        public string TaskName { get; set; }
        public string TrackNote { get; set; }
        public DateTime TrackDate { get; set; }

        protected internal override int PKId => UserId;
    }
}