using System;
using System.Threading.Tasks;

namespace Persistence.Data.Repository.Contract
{
    public interface IExtendedRepository<T> : IGenericRepository<T> where T : class
    {
        Task SoftDeleteRowAsync(Guid id);
    }
}