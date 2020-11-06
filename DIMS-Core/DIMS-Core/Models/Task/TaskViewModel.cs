using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DIMS_Core.Models.Task
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Deadline date is required")]
        public DateTime DeadlineDate { get; set; }

        public List<int> SelectedMembers { get; set; }

        public TaskViewModel()
        {
            SelectedMembers = new List<int>();
        }
    }
}
