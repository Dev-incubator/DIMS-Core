﻿using DIMS_Core.BusinessLayer.Models.UserTask;
using DIMS_Core.DataAccessLayer.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IUserTaskService
    {
        Task<IEnumerable<UserTaskModel>> SearchAsync(UserTaskFilter filter);

        Task<UserTaskModel> GetTaskAsync(int id);

        Task CreateAsync(UserTaskModel model);

        Task DeleteAsync(int id);

        Task UpdateAsync(UserTaskModel model);
    }
}
