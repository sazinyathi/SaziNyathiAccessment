using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TritonExpress.Interfaces.Repositories;
using TritonExpress.Interfaces.Services;
using TritonExpress.Models;

namespace TritonExpress.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository stateRepository;
        public StatusService(IStatusRepository stateRepository)
        {
            this.stateRepository = stateRepository;
        }
        public async Task<int> CreateStatusAsync(Status status)
        {
            return await stateRepository.CreateStatusAsync(status);
        }

        public async Task DeleteStatusAsync(int id)
        {
            await stateRepository.DeleteStatusAsync(id);
        }

        public async Task<IEnumerable<Status>> GetAllStatusAsync()
        {
            return await stateRepository.GetAllStatusAsync();
        }

        public async Task<Status> GetStatusIDAsync(int id)
        {
            return await stateRepository.GetStatusIDAsync(id);
        }

        public async Task UpdateStatusAsync(Status status)
        {
            await stateRepository.UpdateStatusAsync(status);
        }
    }
}
