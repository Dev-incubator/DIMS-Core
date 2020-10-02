using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.DataAccessLayer.Filters;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<MemberModel>> SearchAsync()
        {
            var query = unitOfWork.VUserProfileRepository.Search();
            var mappedQuery = mapper.ProjectTo<MemberModel>(query);

            return await mappedQuery.ToListAsync();
        }

        public Task<MemberModel> GetMemberAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(MemberModel model)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(MemberModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
