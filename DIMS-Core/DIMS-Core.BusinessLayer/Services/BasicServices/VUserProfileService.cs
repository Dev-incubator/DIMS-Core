using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Services
{
    public class VUserProfileService : BasicCRUDService<VUserProfile, VUserProfileModel>, IVUserProfileService
    {
        private protected override IRepository<VUserProfile> BaseRepository => unitOfWork.VUserProfileRepository;

        public VUserProfileService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<VUserProfileModel> GetUserProfileViewById(int userId)
        {
            var entity = await unitOfWork.VUserProfileRepository.GetByIdAsync(userId);
            var vUserModel = mapper.Map<VUserProfileModel>(entity);
            return vUserModel;
        }

        public async Task<VUserProfileModel> GetUserProfileViewByEmail(string email)
        {
            var entity = await unitOfWork.VUserProfileRepository.GetVUserProfileByEmail(email);
            var vUserModel = mapper.Map<VUserProfileModel>(entity);
            return vUserModel;
        }

        public async Task<IEnumerable<VUserProfileModel>> GetUserProfileViewsByDirection(string direction)
        {
            var entity = unitOfWork.VUserProfileRepository.GetVUserProfilesByDirection(direction);
            var vUserModel = mapper.ProjectTo<VUserProfileModel>(entity);
            return await vUserModel.ToListAsync();
        }
    }
}