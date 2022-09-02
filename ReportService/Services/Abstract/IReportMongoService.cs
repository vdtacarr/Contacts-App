using Shared.Events;
using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Services
{
    public interface IReportMongoService
    {
        Task<ReportCreatedEvent> CreateReportAsync();
        Task<List<Report>> GetAllReports();
        Task<Report> GetReportByIdAsync(string id);
    }
}
