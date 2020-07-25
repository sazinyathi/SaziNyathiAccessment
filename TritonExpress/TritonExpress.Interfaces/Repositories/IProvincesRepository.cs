using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.Interfaces.Repositories
{
    public interface IProvincesRepository
    {
        Task<IEnumerable<Province>> GetAllProvinceAsync();
        Task<Province> GetProvinceIDAsync(int id);
        Task<int> CreateProvinceAsync(Province province);
        Task UpdateProvinceAsync(Province province);
        Task DeleteProvinceAsync(int id);
    }
}
