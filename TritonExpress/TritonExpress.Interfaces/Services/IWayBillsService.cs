using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.Interfaces.Services
{
    public  interface IWayBillsService
    {
        Task<IEnumerable<WayBills>> GetAllWayBillsAsync();
        Task<int> CreateWayBillsAsync(WayBills wayBills);
    }
}
