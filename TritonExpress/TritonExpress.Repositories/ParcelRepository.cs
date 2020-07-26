using System.Threading.Tasks;
using TritonExpress.Interfaces.Repositories;
using TritonExpress.Models;

namespace TritonExpress.Repositories
{
    class ParcelRepository : IParcelRepository
    {

        private readonly TritonExpressDbContext dbContext;
        public ParcelRepository(TritonExpressDbContext tritonExpressDbContext)
        {
            this.dbContext = tritonExpressDbContext;
        }
        
        public async Task<int> CreateParcelAsync(Parcel parcel)
        {
            dbContext.Add(parcel);
            await dbContext.SaveChangesAsync();
            return parcel.Id;
        }
    }
}
