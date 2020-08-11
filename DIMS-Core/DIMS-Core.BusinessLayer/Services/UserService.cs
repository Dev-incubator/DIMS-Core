using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.Common.Enums;
using Microsoft.AspNetCore.Identity;
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

        public async Task<IdentityResult> RegistAsync(UserRegistModel model)
        {
            var identityModel = mapper.Map<SignUpModel>(model);
            var result = await userIdentityService.SignUpAsync(identityModel);

            if (result.Succeeded)
            {
                if (model.UserRole == Role.Member)
                {
                    var userProfileModel = mapper.Map<UserProfileModel>(model);
                    userProfileModel.UserId = identityModel.Id;
                    await userProfileService.CreateAsync(userProfileModel);
                }
            }

            return result;
        }

        public async Task<SignInResult> SignInAsync(SignInModel model)
        {
            var result = await userIdentityService.SignInAsync(model);
            return result;
        }

        public async Task SignOutAsync()
        {
            await userIdentityService.SignOutAsync();
        }

        public void Dispose()
        {
            userIdentityService.Dispose();
        }
    }
}