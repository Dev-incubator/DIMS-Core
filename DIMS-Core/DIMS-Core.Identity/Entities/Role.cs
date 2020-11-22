using Microsoft.AspNetCore.Identity;

namespace DIMS_Core.Identity.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role(string name) : base(name)
        {

        }
    }
}
