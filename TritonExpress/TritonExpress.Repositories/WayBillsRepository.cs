using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Interfaces.Repositories;
using TritonExpress.Models;

namespace TritonExpress.Repositories
{
    public class WayBillsRepository : IWayBillsRepository
    {

        private readonly TritonExpressDbContext dbContext;
        public WayBillsRepository(TritonExpressDbContext tritonExpressDbContext)
        {
            this.dbContext = tritonExpressDbContext;
        }
        public async Task<int> CreateProvinceAsync(WayBills wayBills)
        {
            dbContext.Add(wayBills);
            await dbContext.SaveChangesAsync();
            return wayBills.Id;
        }

        public async Task<IEnumerable<WayBills>> GetAllProvinceAsync()
        {
            return await dbContext.WayBills.AsNoTracking().ToListAsync();
        }
    }
}
