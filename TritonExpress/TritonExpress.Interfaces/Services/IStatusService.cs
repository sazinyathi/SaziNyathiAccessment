using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.Interfaces.Services
{
    public interface IStatusService
    {
        Task<IEnumerable<Status>> GetAllStatusAsync();
        Task<Status> GetStatusIDAsync(int id);
        Task<int> CreateStatusAsync(Status status);
        Task UpdateStatusAsync(Status status);
        Task DeleteStatusAsync(int id);
    }
}
