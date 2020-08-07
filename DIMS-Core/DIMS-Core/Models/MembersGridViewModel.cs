using System.Collections.Generic;

namespace DIMS_Core.Models
{
    public class MembersGridViewModel
    {
        public IEnumerable<vUserProfileViewModel> vUserProfileViewModels { get; set; }
        public UserProfileEditViewModel userProfileEditViewModel { get; set; }
    }
}