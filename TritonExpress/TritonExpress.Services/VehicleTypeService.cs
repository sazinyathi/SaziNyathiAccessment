using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Interfaces.Repositories;
using TritonExpress.Interfaces.Services;
using TritonExpress.Models;

namespace TritonExpress.Services
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private IVehicleTypeRepository vehicleTypeRepository;
        public VehicleTypeService(IVehicleTypeRepository vehicleTypeRepository)
        {
            this.vehicleTypeRepository = vehicleTypeRepository;
        }
        public async Task<int> CreateVehicleTypeAsync(VehicleType vehicleType)
        {
            return await vehicleTypeRepository.CreateVehicleTypeAsync(vehicleType);
        }

        public async Task DeleteVehicleTypeAsync(int id)
        {
            await vehicleTypeRepository.DeleteVehicleTypeAsync(id);
        }

        public async Task<IEnumerable<VehicleType>> GetAllVehicleTypeAsync()
        {
            return await vehicleTypeRepository.GetAllVehicleTypeAsync();
        }

        public async Task<VehicleType> GetVehicleTypeIDAsync(int id)
        {
            return await vehicleTypeRepository.GetVehicleTypeAsync(id);
        }

        public async Task UpdateVehicleTypeAsync(VehicleType vehicleType)
        {
            await vehicleTypeRepository.UpdateVehicleTypeAsync(vehicleType);
        }
    }
}
