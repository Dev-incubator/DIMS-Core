using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace DIMS_Core.Models
{
    public class TaskEditViewModel
    {
        public int? TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public IEnumerable<int> UsersAtTask { get; set; }
        public MultiSelectList AllMembers { get; set; }
    }
}