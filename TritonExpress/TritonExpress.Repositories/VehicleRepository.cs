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
    public class VehicleRepository : IVehicleRepository
    {

        private readonly TritonExpressDbContext dbContext;
        public VehicleRepository(TritonExpressDbContext tritonExpressDbContext)
        {
            this.dbContext = tritonExpressDbContext;
        }
        public async Task<int> CreateVehicleAsync(Vehicle vehicle)
        {
            dbContext.Add(vehicle);
            await dbContext.SaveChangesAsync();
            return vehicle.Id;
        }

        public async Task DeleteVehicleAsync(int id)
        {
            var eventEntity = await dbContext.Vechicles.FirstOrDefaultAsync(a => a.Id == id);
            if (eventEntity == null || eventEntity == default)
            {
                throw new KeyNotFoundException($"Id of '{id}' was not found!");
            }
            eventEntity.IsDeleted = true;
            dbContext.Update(eventEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehicleAsync()
        {
            return await dbContext.Vechicles.AsNoTracking().ToListAsync();
        }

        public async Task<Vehicle> GetVehicleAsync(int id)
        {
            return await dbContext.Vechicles.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            var eventEntity = await dbContext.Vechicles.FirstOrDefaultAsync(a => a.Id == vehicle.Id);
            if (eventEntity == null || eventEntity == default)
            {
                throw new KeyNotFoundException($"Id of '{vehicle.Id}' was not found!");
            }
            eventEntity.Make = vehicle.Make;
            eventEntity.Model = vehicle.Model;
            eventEntity.IsDeleted = vehicle.IsDeleted;
            eventEntity.Name = vehicle.Name;
            eventEntity.RegistrationNumber = vehicle.RegistrationNumber;
            eventEntity.VehicleTypeId = vehicle.VehicleTypeId;
           

            dbContext.Update(eventEntity);
            await dbContext.SaveChangesAsync();
        }
    }
}
