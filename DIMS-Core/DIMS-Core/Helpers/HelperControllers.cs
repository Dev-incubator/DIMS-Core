using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.BusinessLayer.Models.UserTask;
using DIMS_Core.Models.Member;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Helpers
{
    public static class HelperControllers
    {
        public static async Task<IEnumerable<MemberViewModel>> GetMembersViewModel(this IMemberService service, IMapper mapper)
        {
            var members = await service.GetAll();
            return mapper.Map<IEnumerable<MemberViewModel>>(members);
        }
    }
}
