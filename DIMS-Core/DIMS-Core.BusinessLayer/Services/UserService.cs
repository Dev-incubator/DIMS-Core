using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserIdentityService userIdentityService;
        private readonly IUserProfileService userProfileService;
        private readonly IMapper mapper;
        public UserService(IUserIdentityService userIdentityService, IUserProfileService userProfileService, IMapper mapper)
        {
            this.userIdentityService = userIdentityService;
            this.userProfileService = userProfileService;
            this.mapper = mapper;
        }
        public async Task DeleteAsync(int identityId)
        {
            var result = await userIdentityService.DeleteAsync(identityId);
            if (result.Succeeded)
            {
                await userProfileService.DeleteAsync(identityId);
            }
        }

        public Task RegistAsync(UserRegistModel model)
        {
            throw new NotImplementedException();
        }
    }
}
