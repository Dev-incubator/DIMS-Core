using DIMS_Core.BusinessLayer.Models.BaseModels;
using System;
using System.Collections.Generic;

namespace DIMS_Core.BusinessLayer.Models.TaskManagerModels
{
    public class TaskCreateModel
    {
        public int? TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public IEnumerable<VUserProfileModel> UsersAtTask { get; set; } = new List<VUserProfileModel>();
        public IEnumerable<VUserProfileModel> AllUsers { get; set; } = new List<VUserProfileModel>();
    }
}