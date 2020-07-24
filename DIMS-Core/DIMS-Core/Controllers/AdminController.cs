using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DIMS_Core.BusinessLayer.Interfaces;
using AutoMapper;
using DIMS_Core.Models.Admin;
using DIMS_Core.BusinessLayer.Models.User;

namespace DIMS_Core.Controllers
{
    public class AdminController : Controller
    {
        private readonly IVUserProfileService userProfileViewService;
        //private readonly IUserProfileManageService userProfileManageService;
        private readonly IMapper mapper;
        public AdminController(IVUserProfileService userProfileViewService, /*IUserProfileManageService userProfileManageService,*/ IMapper mapper)
        {
            this.userProfileViewService = userProfileViewService;
            //this.userProfileManageService = userProfileManageService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var m = (await userProfileViewService.GetAllUserProfileViews()).First();
            var model =  await userProfileViewService.GetEntityModel<VUserProfileModel>(2);
            var mappedModel = mapper.Map<VUserProfileViewModel>(model);
            return View("Index", mappedModel);
        }

        //public async Task<IActionResult> UserProfiles()
        //{
        //}
    }
}