using DIMS_Core.DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Models.Member
{
    public class MemberViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Direction { get; set; }
        public string Education { get; set; }
        public int? Age { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? StartDate { get; set; }
    }
}
