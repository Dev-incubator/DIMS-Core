using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DIMS_Core.BusinessLayer.Models.Account
{
    public class SignInModel
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}