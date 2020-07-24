using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DIMS_Core.BusinessLayer.Interfaces.Admin;
using AutoMapper;
using DIMS_Core.Models.Admin;

namespace DIMS_Core.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserProfileViewService userProfileViewService;
        //private readonly IUserProfileManageService userProfileManageService;
        private readonly IMapper mapper;
        public AdminController(IUserProfileViewService userProfileViewService, /*IUserProfileManageService userProfileManageService,*/ IMapper mapper)
        {
            this.userProfileViewService = userProfileViewService;
            //this.userProfileManageService = userProfileManageService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var model =  (await userProfileViewService.GetAllUserProfileViews()).First();
            var mappedModel = mapper.Map<VUserProfileViewModel>(model);
            return View("Index", mappedModel);
        }

        //public async Task<IActionResult> UserProfiles()
        //{
        //}
    }
}