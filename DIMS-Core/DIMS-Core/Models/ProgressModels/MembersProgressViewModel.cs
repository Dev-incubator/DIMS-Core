using DIMS_Core.BusinessLayer.Models.BaseModels;
using System.Collections.Generic;

namespace DIMS_Core.Models.ProgressModels
{
    public class MembersProgressViewModel
    {
        public IEnumerable<VUserProgressModel> vUserProgressModels { get; set; }
        public string UserName { get; set; }
    }
}