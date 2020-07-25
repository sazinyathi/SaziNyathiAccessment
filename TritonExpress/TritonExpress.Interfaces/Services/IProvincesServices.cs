using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.Interfaces.Services
{
    public interface IProvincesServices
    {
        Task<IEnumerable<Province>> GetAllProvinceAsync();
        Task<Province> GetProvinceIDAsync(int id);
        Task<int> CreateProvinceAsync(Province province);
        Task UpdateProvinceAsync(Province updatedProvince);
        Task DeleteProvinceAsync(int id);
    }
}
