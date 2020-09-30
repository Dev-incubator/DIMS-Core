using System;
using System.Threading.Tasks;

namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISampleRepository SampleRepository { get; }
        Task SaveAsync();
    }
}