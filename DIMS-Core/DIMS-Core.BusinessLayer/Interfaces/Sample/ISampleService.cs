using DIMS_Core.BusinessLayer.Models.Samples;
using DIMS_Core.DataAccessLayer.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface ISampleService
    {
        Task<IEnumerable<SampleModel>> SearchAsync(SampleFilter searchFilter);

        Task<SampleModel> GetSampleAsync(int id);

        Task CreateAsync(SampleModel model);

        Task DeleteAsync(int id);

        Task UpdateAsync(SampleModel model);
    }
}