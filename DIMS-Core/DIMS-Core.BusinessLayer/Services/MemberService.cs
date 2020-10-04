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

        public async Task<IEnumerable<VUserProfileModel>> SearchAsync()
        {
            var query = unitOfWork.VUserProfileRepository.Search();
            var mappedQuery = mapper.ProjectTo<VUserProfileModel>(query);

            return await mappedQuery.ToListAsync();
        }

        public async Task<VUserProfileModel> GetMemberAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var entity = await unitOfWork.VUserProfileRepository.GetByIdAsync(id);
            var model = mapper.Map<VUserProfileModel>(entity);

            return model;
        }

        public async Task CreateAsync(VUserProfileModel model)
        {
            if (model is null || model.UserId != 0)
            {
                return;
            }

            var entity = mapper.Map<EntityUserProfile>(model);

            await unitOfWork.UserProfileRepository.CreateAsync(entity);

            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(VUserProfileModel model)
        {
            if (model is null || model.UserId <= 0)
            {
                return;
            }

            var entity = await unitOfWork.UserProfileRepository.GetByIdAsync(model.UserId);

            if (entity is null)
            {
                return;
            }

            var mappedEntity = mapper.Map(model, entity);

            unitOfWork.UserProfileRepository.Update(mappedEntity);

            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return;
            }

            await unitOfWork.UserProfileRepository.DeleteAsync(id);

            await unitOfWork.SaveAsync();
        }

    }
}
