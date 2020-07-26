using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.Interfaces.Services
{
    public interface IParcelService
    {
        Task<int> CreateParcelAsync(Parcel parcel);
    }
}
