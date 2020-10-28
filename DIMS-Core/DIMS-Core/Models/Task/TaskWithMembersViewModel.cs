using DIMS_Core.BusinessLayer.Models.Task;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DIMS_Core.Models.Task
{
    public class TaskWithMembersViewModel
    {
        public int TaskId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Deadline date is required")]
        public DateTime DeadlineDate { get; set; }

        public List<MemberForTaskModel> Members { get; set; } = new List<MemberForTaskModel>();

        public TaskWithMembersViewModel() { }

        public TaskWithMembersViewModel(List <MemberForTaskModel> Members)
        {
            StartDate = DateTime.Now;
            DeadlineDate = DateTime.Now;
            this.Members = Members;
        }
    }
}
