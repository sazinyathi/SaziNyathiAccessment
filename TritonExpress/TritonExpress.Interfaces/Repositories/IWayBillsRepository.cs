using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.Interfaces.Repositories
{
    public interface IWayBillsRepository
    {
        Task<IEnumerable<WayBills>> GetAllProvinceAsync();
        Task<int> CreateProvinceAsync(WayBills wayBills);
    }
}
