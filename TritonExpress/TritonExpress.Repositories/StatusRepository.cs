using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TritonExpress.Interfaces.Repositories;
using TritonExpress.Models;

namespace TritonExpress.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly TritonExpressDbContext dbContext;
        public StatusRepository(TritonExpressDbContext tritonExpressDbContext)
        {
            this.dbContext = tritonExpressDbContext;
        }

        public async Task<int> CreateStatusAsync(Status status)
        {
            dbContext.Add(status);
            await dbContext.SaveChangesAsync();
            return status.Id;
        }

        public async Task DeleteStatusAsync(int id)
        {
            var eventEntity = await dbContext.Statuses.FirstOrDefaultAsync(a => a.Id == id);
            if (eventEntity == null || eventEntity == default)
            {
                throw new KeyNotFoundException($"Id of '{id}' was not found!");
            }
            eventEntity.IsDeleted = true;
            dbContext.Update(eventEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Status>> GetAllStatusAsync()
        {
            return await dbContext.Statuses.AsNoTracking().ToListAsync();
        }

        public async Task<Status> GetStatusIDAsync(int id)
        {
            return await dbContext.Statuses.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateStatusAsync(Status status)
        {
            var eventEntity = await dbContext.Statuses.FirstOrDefaultAsync(a => a.Id == status.Id);
            if (eventEntity == null || eventEntity == default)
            {
                throw new KeyNotFoundException($"Id of '{status.Id}' was not found!");
            }
         
            eventEntity.IsDeleted = status.IsDeleted;
            eventEntity.Name = status.Name;
            eventEntity.Description = status.Description;

            dbContext.Update(eventEntity);
            await dbContext.SaveChangesAsync();
        }
    }
}
