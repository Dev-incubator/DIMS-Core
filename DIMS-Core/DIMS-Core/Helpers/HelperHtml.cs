using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.Models.Member;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DIMS_Core.Helpers
{
    public static class HelperHtml
    {
        public static HtmlString CreateCheckListMembers(this IHtmlHelper html, 
            List<int> selectedMembers, List<MemberViewModel> allMembers)
        {
            string result = "<div class=\"form-control checkbox-list\">\n";
            if (allMembers != null && allMembers.Count > 0)
            {
                for (int i = 0; i < allMembers.Count; i++)
                {
                    result +=
                        "<div class=\"item\">"
                            + $"<input type=\"hidden\" name=\"SelectedMembers\" value=\"{allMembers[i].UserId}\" data-val=\"true\" disabled>"
                            + "<label>"
                                + $"<input type=\"checkbox\""
                                    + $"{(selectedMembers.Contains(allMembers[i].UserId) ? " checked=\"checked\"" : null)}" 
                                + $">"
                                + $"<span>{allMembers[i].FullName}</span>"
                            + "</label>"
                        + "</div>";
                }
            }
            result += "</div>";
            return new HtmlString(result);
        }

        public static HtmlString CreateSimpleListMembers(this IHtmlHelper html,
            List<int> selectedMembers, List<MemberViewModel> allMembers)
        {
            if (selectedMembers is null || selectedMembers.Count == 0)
            {
                return new HtmlString("<div>-</div>");
            }
            else
            {
                string result = string.Empty;
                foreach (var selectedMember in selectedMembers)
                {
                    result += $"<div>{allMembers.Find(x => x.UserId == selectedMember).FullName}</div>";
                }
                return new HtmlString(result);
            }
        }

        public static HtmlString DisplayStringOrNull(this IHtmlHelper html, string value)
        {
            return new HtmlString(value ?? "-");
        }
    }
}
