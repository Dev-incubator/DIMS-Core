using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserProfileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task CreateUserProfile(UserProfileModel userModel)
        {
            if (userModel is null)
            {
                return;
            }
        }

        public Task DeleteUserProfile(int userProfileId)
        {
            throw new NotImplementedException();
        }

        public Task GetUserProfile(int userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserProfile(UserProfileModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}
