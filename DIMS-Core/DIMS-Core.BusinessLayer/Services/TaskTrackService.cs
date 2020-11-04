using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.TaskTrack;
using System.Linq;

namespace DIMS_Core.BusinessLayer.Services
{
    internal class TaskTrackService : ITaskTrackService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TaskTrackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<VUserTrackModel>> GetAllByUserId(int userId)
        {
            var taskTracks = unitOfWork.VUserTrackRepository.GetAll().Where(x => x.UserId == userId);
            var mappedQuery = mapper.ProjectTo<VUserTrackModel>(taskTracks);

            return await mappedQuery.ToListAsync();
        }
    }
}
