using DIMS_Core.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace DIMS_Core.Models
{
    public class UserProfileEditViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }

        [Required]
        public int DirectionId { get; set; }

        [Required]
        [MinLength(2)]
        [Display()]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        public Gender Sex { get; set; }
        
        public string Skype { get; set; }
        
        [Phone]
        public string MobilePhone { get; set; }
    }
}