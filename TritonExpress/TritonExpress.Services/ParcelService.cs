using System.Threading.Tasks;
using TritonExpress.Interfaces.Repositories;
using TritonExpress.Interfaces.Services;
using TritonExpress.Models;

namespace TritonExpress.Services
{
    public class ParcelService : IParcelService
    {
        private readonly IParcelRepository parcelRepository;
        public ParcelService(IParcelRepository parcelRepository)
        {
            this.parcelRepository = parcelRepository;
        }

        public async Task<int> CreateParcelAsync(Parcel parcel)
        {
            return await parcelRepository.CreateParcelAsync(parcel);
        }
    }
}
