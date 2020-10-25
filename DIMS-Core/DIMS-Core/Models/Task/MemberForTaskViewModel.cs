using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Models.Task
{
    public class MemberForTaskViewModel
    {
        public int UserTaskId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public bool Selected { get; set; }
    }
}
