using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TritonExpress.Interfaces.Repositories;
using TritonExpress.Models;

namespace TritonExpress.Repositories
{
    public class ProvincesRepository : IProvincesRepository
    {
        private readonly TritonExpressDbContext dbContext;
        public ProvincesRepository(TritonExpressDbContext tritonExpressDbContext)
        {
            this.dbContext = tritonExpressDbContext;
        }
        public async Task<int> CreateProvinceAsync(Province province)
        {
            dbContext.Add(province);
            await dbContext.SaveChangesAsync();
            return province.Id;
        }

        public async Task DeleteProvinceAsync(int id)
        {
            var eventEntity = await dbContext.Provinces.FirstOrDefaultAsync(a => a.Id == id);
            if (eventEntity == null || eventEntity == default)
            {
                throw new KeyNotFoundException($"Id of '{id}' was not found!");
            }
            eventEntity.IsDeleted = true;
            dbContext.Update(eventEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Province>> GetAllProvinceAsync()
        {
            return await dbContext.Provinces.AsNoTracking().ToListAsync();
        }

        public async Task<Province> GetProvinceIDAsync(int id)
        {
            return await dbContext.Provinces.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateProvinceAsync(Province province)
        {
            var eventEntity = await dbContext.Provinces.FirstOrDefaultAsync(a => a.Id == province.Id);
            if (eventEntity == null || eventEntity == default)
            {
                throw new KeyNotFoundException($"Id of '{province.Id}' was not found!");
            }
            eventEntity.Branches = province.Branches;
            eventEntity.Description = province.Description;
            eventEntity.IsDeleted = province.IsDeleted;
            eventEntity.Name = province.Name;
        

            dbContext.Update(eventEntity);
            await dbContext.SaveChangesAsync();
        }
    }
}
