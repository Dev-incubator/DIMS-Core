using DIMS_Core.BusinessLayer.Models.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DIMS_Core.Models
{
    public class UserProfileEditViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int DirectionId { get; set; }
        public string MobilePhone { get; set; }
    }
}