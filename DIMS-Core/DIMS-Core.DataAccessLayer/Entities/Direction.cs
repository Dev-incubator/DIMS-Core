using System;
using System.Collections.Generic;

namespace DIMS_Core.DataAccessLayer.Entities
{
    public partial class Direction
    {
        public Direction()
        {
            UserProfile = new HashSet<UserProfile>();
        }

        public int DirectionId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<UserProfile> UserProfile { get; set; }
    }
}
