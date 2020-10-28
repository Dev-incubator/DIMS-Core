using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.BusinessLayer.Models.UserTask;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Helpers
{
    public static class HelperControllers
    {
        public static void GetMembersForTask(this ICollection<MemberForTaskModel> members, ICollection<UserTaskModel> userTasks)
        {
            foreach (var userTask in userTasks)
                if (userTask.User != null)
                    members.Add(new MemberForTaskModel()
                    {
                        UserId = userTask.UserId,
                        FullName = userTask.User.Name + " " + userTask.User.LastName,
                        Selected = true,
                        UserTaskId = userTask.UserTaskId
                    });
        }

        public static void MarkSelectedMembersForTask(this ICollection<MemberForTaskModel> allMembers, ICollection<UserTaskModel> userTasks)
        {
            foreach (var userTask in userTasks)
                if (userTask.User != null)
                {
                    var member = allMembers.FirstOrDefault(user => user.UserId == userTask.UserId);
                    member.Selected = true;
                    member.UserTaskId = userTask.UserTaskId;
                }
        }
    }
}
