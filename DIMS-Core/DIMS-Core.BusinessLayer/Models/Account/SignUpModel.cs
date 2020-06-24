using System.ComponentModel.DataAnnotations;

namespace DIMS_Core.BusinessLayer.Models.Account
{
    public class SignUpModel
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [MinLength(5)]
        public string ConfirmPassword { get; set; }
    }
}