using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.Interfaces.Repositories
{
    public interface IBranchesRepository
    {
        Task<IEnumerable<Branches>> GetAllBranchesAsync();
        Task<Branches> GetBranchesIDAsync(int id);
        Task<int> CreateBranchesAsync(Branches branches);
        Task UpdateBranchesAsync(Branches branches);
        Task DeleteBranchesAsync(int id);
    }
}
