using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Interfaces.Repositories;
using TritonExpress.Interfaces.Services;
using TritonExpress.Models;

namespace TritonExpress.Services
{
    public class VehicleService : IVehicleService
    {
        private IVehicleRepository vehicleRepository;
        public VehicleService(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }
        public async Task<int> CreateVehicleAsync(Vehicle vehicle)
        {
            return await vehicleRepository.CreateVehicleAsync(vehicle);
        }

        public async Task DeleteVehicleAsync(int id)
        {
            await vehicleRepository.DeleteVehicleAsync(id);
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehicleAsync()
        {
            return await vehicleRepository.GetAllVehicleAsync();
        }

        public async Task<Vehicle> GetVehicleIDAsync(int id)
        {
            return await vehicleRepository.GetVehicleAsync(id);
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            await vehicleRepository.UpdateVehicleAsync(vehicle);
        }
    }
}
