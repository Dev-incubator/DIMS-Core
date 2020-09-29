using DIMS_Core.DataAccessLayer.Enums;
using System;
using System.Collections.Generic;

namespace DIMS_Core.DataAccessLayer.Entities
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            UserTask = new HashSet<UserTask>();
        }

        public int UserId { get; set; }
        public int DirectionId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public Sex Sex { get; set; }
        public string Education { get; set; }
        public DateTime? BirthOfDate { get; set; }
        public double? UniversityAverageScore { get; set; }
        public double? MathScore { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string Skype { get; set; }
        public DateTime? StartDate { get; set; }

        public virtual Direction Direction { get; set; }
        public virtual ICollection<UserTask> UserTask { get; set; }
    }
}
