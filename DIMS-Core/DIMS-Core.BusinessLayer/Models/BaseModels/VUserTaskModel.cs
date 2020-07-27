using System;

namespace DIMS_Core.BusinessLayer.Models.BaseModels
{
    public class VUserTaskModel : BaseDTOModel
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public string State { get; set; }

        protected internal override int PKId => UserId;
    }
}