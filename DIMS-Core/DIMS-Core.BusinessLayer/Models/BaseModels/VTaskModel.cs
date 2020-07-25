using System;

namespace DIMS_Core.BusinessLayer.Models.BaseModels
{
    public class VTaskModel : BaseDTOModel
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }

        protected internal override int PKId => TaskId;
    }
}