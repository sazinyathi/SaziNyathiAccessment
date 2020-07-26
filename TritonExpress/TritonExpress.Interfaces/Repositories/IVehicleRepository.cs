using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.Interfaces.Repositories
{
   public  interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAllVehicleAsync();
        Task<Vehicle> GetVehicleAsync(int id);
        Task<int> CreateVehicleAsync(Vehicle  vehicle);
        Task UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(int id);
    }
}
