using System;
using System.Collections.Generic;
using System.Text;

namespace DIMS_Core.BusinessLayer.Models.Task
{
    public class MemberForTaskModel
    {
        public int UserTaskId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public bool Selected { get; set; }
    }
}
