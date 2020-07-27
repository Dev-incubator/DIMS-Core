using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Models
{
    public class MembersGridViewModel
    {
        public IEnumerable<vUserProfileViewModel> vUserProfileViewModels { get; set; }
        public UserProfileEditViewModel userProfileEditViewModel { get; set; }
    }
}
