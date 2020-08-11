using DIMS_Core.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace DIMS_Core.Models
{
    public class UserRegistViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required]
        public int DirectionId { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public Gender Sex { get; set; }

        public string Education { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public double UniversityAverageScore { get; set; }
        public double MathScore { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string Skype { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        public Role UserRole { get; set; }

        public SelectList Directions { get; set; }
    }
}