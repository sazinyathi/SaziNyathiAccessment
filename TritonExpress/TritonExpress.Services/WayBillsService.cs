using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TritonExpress.Interfaces.Repositories;
using TritonExpress.Interfaces.Services;
using TritonExpress.Models;

namespace TritonExpress.Services
{
    public class WayBillsService : IWayBillsService
    {
        private readonly IWayBillsRepository wayBillsRepository;

        public WayBillsService(IWayBillsRepository wayBillsRepository)
        {
            this.wayBillsRepository = wayBillsRepository;
        }
        public async Task<int> CreateWayBillsAsync(WayBills wayBills)
        {
            return await wayBillsRepository.CreateProvinceAsync(wayBills);
        }

        public async Task<IEnumerable<WayBills>> GetAllWayBillsAsync()
        {
            return await wayBillsRepository.GetAllProvinceAsync();
        }
    }
}
