using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.Interfaces.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAllVehicleAsync();
        Task<Vehicle> GetVehicleIDAsync(int id);
        Task<int> CreateVehicleAsync(Vehicle vehicle);
        Task UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(int id);
    }
}
