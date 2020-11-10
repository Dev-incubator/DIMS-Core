using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.MappingProfiles;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.DataAccessLayer.Filters;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EntityUserProfile = DIMS_Core.DataAccessLayer.Entities.UserProfile;

namespace DIMS_Core.BusinessLayer.Services
{
    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<VUserProfileModel>> GetAll()
        {
            var vUserProfiles = unitOfWork.VUserProfileRepository.GetAll();
            var vUserProfileModels = mapper.ProjectTo<VUserProfileModel>(vUserProfiles);

            return await vUserProfileModels.ToListAsync();
        }

        public async Task<UserProfileModel> GetMember(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var userProfile = await unitOfWork.UserProfileRepository.GetById(id);
            var userProfileModel = mapper.Map<UserProfileModel>(userProfile);

            return userProfileModel;
        }

        public async Task<UserProfileModel> GetMemberByEmail(string email)
        {
            var userProfile = await unitOfWork.UserProfileRepository.GetMemberByEmail(email);
            var userProfileModel = mapper.Map<UserProfileModel>(userProfile);
            return userProfileModel;

        }

        public async Task Create(UserProfileModel model)
        {
            if (model is null || model.UserId != 0)
            {
                return;
            }

            var userProfile = mapper.Map<EntityUserProfile>(model);

            await unitOfWork.UserProfileRepository.Create(userProfile);

            await unitOfWork.Save();
        }

        public async Task Update(UserProfileModel model)
        {
            if (model is null || model.UserId <= 0)
            {
                return;
            }

            var userProfile = await unitOfWork.UserProfileRepository.GetById(model.UserId);

            if (userProfile is null)
            {
                return;
            }

            var userProfileModel = mapper.Map(model, userProfile);

            unitOfWork.UserProfileRepository.Update(userProfileModel);

            await unitOfWork.Save();
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }

            await unitOfWork.UserProfileRepository.Delete(id);

            await unitOfWork.Save();
        }

    }
}
