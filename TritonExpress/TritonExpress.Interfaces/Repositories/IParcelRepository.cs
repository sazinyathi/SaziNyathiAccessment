using System.Threading.Tasks;
using TritonExpress.Models;

namespace TritonExpress.Interfaces.Repositories
{
    public interface IParcelRepository
    {
        Task<int> CreateParcelAsync(Parcel parcel);
    }
}
