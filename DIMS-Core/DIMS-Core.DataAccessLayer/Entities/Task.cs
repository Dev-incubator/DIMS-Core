﻿using System;
using System.Collections.Generic;

namespace DIMS_Core.DataAccessLayer.Entities
{
    public partial class Task
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }

        public virtual ICollection<UserTask> UserTask { get; set; }

        public Task()
        {
            UserTask = new HashSet<UserTask>();
        }
    }
}
