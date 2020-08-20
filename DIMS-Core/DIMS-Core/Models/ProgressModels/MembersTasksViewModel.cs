using System.Collections.Generic;

namespace DIMS_Core.Models.ProgressModels
{
    public class MembersTasksViewModel
    {
        public string UserName { get; set; }
        public IEnumerable<UserTaskViewModel> userTaskModels { get; set; }
    }
}