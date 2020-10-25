using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.BusinessLayer.Models.UserTask;
using DIMS_Core.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    internal class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskModel, Task>().ReverseMap();
            CreateMap<VTask, TaskModel>().ReverseMap();
            CreateMap<UserTaskModel, UserTask>().ReverseMap();
        }
    }
}
