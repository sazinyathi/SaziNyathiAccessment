using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Interfaces.Repositories;
using TritonExpress.Interfaces.Services;
using TritonExpress.Models;

namespace TritonExpress.Services
{
    public class BranchesService : IBranchesService
    {
        private IBranchesRepository branchRepository;
        public BranchesService(IBranchesRepository branchesRepository)
        {
            this.branchRepository = branchesRepository;
        }
        public async Task<int> CreateBranchesAsync(Branches branches)
        {
            return await branchRepository.CreateBranchesAsync(branches);
        }

        public async Task DeleteBranchesAsync(int id)
        {
            await branchRepository.DeleteBranchesAsync(id);
        }

        public async Task<IEnumerable<Branches>> GetAllBranchesAsync()
        {
            return await branchRepository.GetAllBranchesAsync();
        }

        public async Task<Branches> GetBranchesIDAsync(int id)
        {
            return await branchRepository.GetBranchesIDAsync(id);
        }

        public async Task UpdateBranchesAsync(Branches branches)
        {
            await branchRepository.UpdateBranchesAsync(branches);
        }
    }
}
