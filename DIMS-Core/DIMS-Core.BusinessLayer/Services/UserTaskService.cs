using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.UserTask;
using System.Linq;

namespace DIMS_Core.BusinessLayer.Services
{
    internal class UserTaskService : IUserTaskService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserTaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserTaskModel>> GetAllForMember(int userId)
        {
            var query = unitOfWork.UserTaskRepository.GetAll()
                .Where(userTask => userTask.UserId == userId);

            var mappedQuery = mapper.ProjectTo<UserTaskModel>(query);

            return await mappedQuery.ToListAsync();
        }
    }
}
