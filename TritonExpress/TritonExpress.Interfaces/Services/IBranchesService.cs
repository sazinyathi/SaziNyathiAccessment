using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.Interfaces.Services
{
   public interface IBranchesService
    {
        Task<IEnumerable<Branches>> GetAllBranchesAsync();
        Task<Branches> GetBranchesIDAsync(int id);
        Task<int> CreateBranchesAsync(Branches branches);
        Task UpdateBranchesAsync(Branches branches);
        Task DeleteBranchesAsync(int id);
    }
}
