using System;
using System.Collections.Generic;
using System.Text;

namespace DIMS_Core.BusinessLayer.Models.UserTask
{
    public class UserTaskModel
    {
        public int UserTaskId { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int StateId { get; set; }
    }
}
