using System;
using System.Collections.Generic;

namespace DIMS_Core.BusinessLayer.Models.BaseModels
{
    public class TaskModel : BaseDTOModel
    {
        public int? TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public ICollection<UserTaskModel> UserTask { get; set; }

        protected internal override int PKId => TaskId.Value;
    }
}