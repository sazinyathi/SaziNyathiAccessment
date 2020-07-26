using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.Interfaces.Repositories
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> GetAllStatusAsync();
        Task<Status> GetStatusIDAsync(int id);
        Task<int> CreateStatusAsync(Status status);
        Task UpdateStatusAsync(Status status);
        Task DeleteStatusAsync(int id);
    }
}
