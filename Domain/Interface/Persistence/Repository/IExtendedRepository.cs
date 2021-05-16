using System;
using System.Threading.Tasks;
using Domain.Interface.Persistence.Repository;

namespace Persistence.Data.Repository.Contract
{
    public interface IExtendedRepository<T> : IGenericRepository<T> where T : class
    {
        Task SoftDeleteRowAsync(Guid id);
    }
}