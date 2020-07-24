using System;

namespace DIMS_Core.BusinessLayer.Models.User
{
    public class UserProfileModel
    {
        public int UserId { get; set; }
        public int? DirectionId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Education { get; set; }
        public DateTime? BirthDate { get; set; }
        public double? UniversityAverageScore { get; set; }
        public double? MathScore { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string Skype { get; set; }
        public DateTime? StartDate { get; set; }
    }
}