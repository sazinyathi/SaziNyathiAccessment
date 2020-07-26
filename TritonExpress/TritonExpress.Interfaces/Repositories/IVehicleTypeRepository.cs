using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.Interfaces.Repositories
{
    public interface IVehicleTypeRepository
    {
        Task<IEnumerable<VehicleType>> GetAllVehicleTypeAsync();
        Task<VehicleType> GetVehicleTypeAsync(int id);
        Task<int> CreateVehicleTypeAsync(VehicleType vehicle);
        Task UpdateVehicleTypeAsync(VehicleType vehicle);
        Task DeleteVehicleTypeAsync(int id);
    }
}
