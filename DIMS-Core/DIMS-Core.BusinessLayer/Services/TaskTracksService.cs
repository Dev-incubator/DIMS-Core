using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.TaskTracks;

namespace DIMS_Core.BusinessLayer.Services
{
    internal class TaskTracksService : ITaskTracksService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TaskTracksService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<VUserTrackModel>> GetAll()
        {
            var taskTracks = unitOfWork.VUserTrackRepository.GetAll();
            var mappedQuery = mapper.ProjectTo<VUserTrackModel>(taskTracks);

            return await mappedQuery.ToListAsync();
        }
    }
}
