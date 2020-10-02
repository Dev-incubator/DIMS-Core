﻿using DIMS_Core.DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIMS_Core.BusinessLayer.Models.Members
{
    public class MemberModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Direction { get; set; }
        public Sex Sex { get; set; }
        public string Education { get; set; }
        public int? Age { get; set; }
        public double? UniversityAverageScore { get; set; }
        public double? MathScore { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string Skype { get; set; }
        public DateTime? StartDate { get; set; }
    }
}