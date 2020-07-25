using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Interfaces.Repositories;
using TritonExpress.Interfaces.Services;
using TritonExpress.Models;

namespace TritonExpress.Services
{
    public class ProvincesServices : IProvincesServices
    {
        private readonly IProvincesRepository  provincesRepository;
        public ProvincesServices(IProvincesRepository  provincesRepository)
        {
            this.provincesRepository = provincesRepository;
        }

        public async Task<int> CreateProvinceAsync(Province province)
        {
            return await provincesRepository.CreateProvinceAsync(province);
        }

        public async Task DeleteProvinceAsync(int id)
        {
            await provincesRepository.DeleteProvinceAsync(id);
        }

        public async Task<IEnumerable<Province>> GetAllProvinceAsync()
        {
            return await provincesRepository.GetAllProvinceAsync();
        }

        public async Task<Province> GetProvinceIDAsync(int id)
        {
            return await provincesRepository.GetProvinceIDAsync(id);
        }

        public async Task UpdateProvinceAsync(Province updatedProvince)
        {
            await provincesRepository.UpdateProvinceAsync(updatedProvince);
        }
    }
}
