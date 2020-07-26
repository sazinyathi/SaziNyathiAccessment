using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.Interfaces.Services
{
    public interface IVehicleTypeService
    {
        Task<IEnumerable<VehicleType>> GetAllVehicleTypeAsync();
        Task<VehicleType> GetVehicleTypeIDAsync(int id);
        Task<int> CreateVehicleTypeAsync(VehicleType vehicleType);
        Task UpdateVehicleTypeAsync(VehicleType vehicleType);
        Task DeleteVehicleTypeAsync(int id);
    }
}
