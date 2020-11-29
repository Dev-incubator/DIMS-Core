using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DIMS_Core.Models.Route
{
    public class Route
    {
        [BindingBehavior(BindingBehavior.Optional)]
        public string Controller { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        public string Action { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        public string BackController { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        public string BackAction { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        public int? TaskId { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        public int? UserId { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        public int? UserTaskId { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        public int? TaskTrackId { get; set; }
    }
}
