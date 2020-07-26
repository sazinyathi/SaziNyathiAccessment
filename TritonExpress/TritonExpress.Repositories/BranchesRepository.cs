using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TritonExpress.Interfaces.Repositories;
using TritonExpress.Models;

namespace TritonExpress.Repositories
{
    public class BranchesRepository : IBranchesRepository
    {
        private readonly TritonExpressDbContext dbContext;
        public BranchesRepository(TritonExpressDbContext tritonExpressDbContext)
        {
            this.dbContext = tritonExpressDbContext;
        }
        
        public async Task<int> CreateBranchesAsync(Branches branches)
        {
            dbContext.Add(branches);
            await dbContext.SaveChangesAsync();
            return branches.Id;
        }

        public async Task DeleteBranchesAsync(int id)
        {
            var eventEntity = await dbContext.Branches.FirstOrDefaultAsync(a => a.Id == id);
            if (eventEntity == null || eventEntity == default)
            {
                throw new KeyNotFoundException($"Id of '{id}' was not found!");
            }
            eventEntity.IsDeleted = true;
            dbContext.Update(eventEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Branches>> GetAllBranchesAsync()
        {
            return await dbContext.Branches.Include(x =>x.Vehicle).AsNoTracking().ToListAsync();
        }

        public async Task<Branches> GetBranchesIDAsync(int id)
        {
            return await dbContext.Branches.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateBranchesAsync(Branches branches)
        {
            var eventEntity = await dbContext.Branches.FirstOrDefaultAsync(a => a.Id == branches.Id);
            if (eventEntity == null || eventEntity == default)
            {
                throw new KeyNotFoundException($"Id of '{branches.Id}' was not found!");
            }
            eventEntity.Address = branches.Address;
            eventEntity.BranchDescription = branches.BranchDescription;
            eventEntity.IsDeleted = branches.IsDeleted;
            eventEntity.BranchName = branches.BranchName;
            eventEntity.IsActive = branches.IsActive;
            eventEntity.IsDeleted = branches.IsDeleted;
            eventEntity.ProvincesId = branches.ProvincesId;

            dbContext.Update(eventEntity);
            await dbContext.SaveChangesAsync();
        }
    }
}
