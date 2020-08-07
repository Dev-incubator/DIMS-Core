using System;

namespace DIMS_Core.Models
{
    public class vUserProfileViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Direction { get; set; }
        public string Education { get; set; }
        public DateTime StartDate { get; set; }
        public int Age { get; set; }
    }
}