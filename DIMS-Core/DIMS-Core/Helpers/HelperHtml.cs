using DIMS_Core.BusinessLayer.Models.Task;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DIMS_Core.Helpers
{
    public static class HelperHtml
    {
        public static HtmlString CreateMembersList(this IHtmlHelper html, List<MemberForTaskModel> members)
        {
            string result = "<div class=\"form-control checkbox-list\">\n";
            if (members is null || members.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < members.Count; i++)
                {
                    result +=
                        "<div class=\"item\">"
                            + $"<input type=\"hidden\" name=\"Members[{i}].UserTaskId\" value=\"{members[i].UserTaskId}\">"
                            + $"<input type=\"hidden\" name=\"Members[{i}].UserId\" value=\"{members[i].UserId}\">"
                            + $"<input type=\"hidden\" name=\"Members[{i}].FullName\" value=\"{members[i].FullName}\">"
                            + "<label>"
                                + $"<input type=\"checkbox\" name=\"Members[{i}].Selected\" value=\"true\""
                                + $"{(members[i].Selected ? "checked= \"checked\"" : null)}>"
                                + $"<span>{members[i].FullName}</span>"
                            + "</label>"
                        + "</div>";
                }
            }
            result += "</div>";
            return new HtmlString(result);
        }

        public static HtmlString CreateMembersSimpleList(this IHtmlHelper html, List<MemberForTaskModel> members)
        {
            if (members is null || members.Count == 0)
            {
                return new HtmlString("<div>-</div>");
            }
            else
            {
                string result = string.Empty;
                foreach (var member in members)
                {
                    result += $"<div>{member.FullName}</div>";
                }
                return new HtmlString(result);
            }
        }
    }
}
