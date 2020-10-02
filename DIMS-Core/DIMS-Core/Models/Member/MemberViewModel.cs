using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Models.Member
{
    public class MemberViewModel
    {
        public int MemberId { get; set; }

        public string Direction { get; set; }

        public string Education { get; set; }

        public string FullName { get; set; }

        public DateTime StartDate { get; set; }

        public int Age { get; set; }
    }
}
