﻿using DIMS_Core.DataAccessLayer.Enums;
using DIMS_Core.Models.Member;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Models.Task
{
    public class TaskWithMembersViewModel : TaskViewModel
    {
        public List<MemberForTaskViewModel> Members { get; set; }
    }
}