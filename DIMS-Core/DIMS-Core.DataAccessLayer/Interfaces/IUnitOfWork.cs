using DIMS_Core.DataAccessLayer.Context;
using System;
using System.Threading.Tasks;

namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Sample> SampleRepository { get; }
        Task SaveAsync();
    }
}