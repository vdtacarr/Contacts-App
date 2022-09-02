using System.Threading.Tasks;

namespace ProcessService.Services
{
    public interface IProcessMongoService
    {
        Task UpdateReport(string Id);
    }
}
