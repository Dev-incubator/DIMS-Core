using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Models.Admin
{
    public class VUserProfileViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Education { get; set; }
        public string Direction { get; set; }
        public DateTime? StartDate { get; set; }
        public int? Age { get; set; }
    }
}
