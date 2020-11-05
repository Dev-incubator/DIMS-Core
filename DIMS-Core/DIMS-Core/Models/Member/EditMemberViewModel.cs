using DIMS_Core.DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Models.Member
{
    public class EditMemberViewModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Direction is required")]
        public int DirectionId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Sex is required")]
        public Sex Sex { get; set; }
        public string Education { get; set; }
        public DateTime? BirthOfDate { get; set; }
        public double? UniversityAverageScore { get; set; }
        public double? MathScore { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string Skype { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
