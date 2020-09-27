using System;
using System.Collections.Generic;

namespace DIMS_Core.Models
{
    public partial class TaskState
    {
        public TaskState()
        {
            UserTask = new HashSet<UserTask>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }

        public virtual ICollection<UserTask> UserTask { get; set; }
    }
}
