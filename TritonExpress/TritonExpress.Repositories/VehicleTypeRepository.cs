using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TritonExpress.Interfaces.Repositories;
using TritonExpress.Models;

namespace TritonExpress.Repositories
{
    public class VehicleTypeRepository : IVehicleTypeRepository
    {
        private readonly TritonExpressDbContext dbContext;
        public VehicleTypeRepository(TritonExpressDbContext tritonExpressDbContext)
        {
            this.dbContext = tritonExpressDbContext;
        }
        public async Task<int> CreateVehicleTypeAsync(VehicleType vehicle)
        {
            dbContext.Add(vehicle);
            await dbContext.SaveChangesAsync();
            return vehicle.Id;
        }

        public async Task DeleteVehicleTypeAsync(int id)
        {
            var eventEntity = await dbContext.VehicleType.FirstOrDefaultAsync(a => a.Id == id);
            if (eventEntity == null || eventEntity == default)
            {
                throw new KeyNotFoundException($"Id of '{id}' was not found!");
            }
            eventEntity.IsDeleted = true;
            dbContext.Update(eventEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<VehicleType>> GetAllVehicleTypeAsync()
        {
            return await dbContext.VehicleType.AsNoTracking().ToListAsync();
        }

        public async Task<VehicleType> GetVehicleTypeAsync(int id)
        {
            return await dbContext.VehicleType.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateVehicleTypeAsync(VehicleType vehicle)
        {
            var eventEntity = await dbContext.VehicleType.FirstOrDefaultAsync(a => a.Id == vehicle.Id);
            if (eventEntity == null || eventEntity == default)
            {
                throw new KeyNotFoundException($"Id of '{vehicle.Id}' was not found!");
            }
            eventEntity.Descriptions = vehicle.Descriptions;
            eventEntity.Name = vehicle.Name;
            eventEntity.IsDeleted = vehicle.IsDeleted;

            dbContext.Update(eventEntity);
            await dbContext.SaveChangesAsync();
        }
    }
}
