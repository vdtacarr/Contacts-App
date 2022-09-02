using MassTransit;
using ProcessService.Services;
using Shared.Events;
using System.Threading.Tasks;

namespace ProcessService.Customer
{
    public class ReportPrepearedEventConsumer : IConsumer<ReportCreatedEvent>
    {
        readonly IProcessMongoService _mongoService;
        public ReportPrepearedEventConsumer(IProcessMongoService mongoService)
        {
            _mongoService = mongoService;
        }

        public async Task Consume(ConsumeContext<ReportCreatedEvent> context)
        {
            var id = context.Message.Id;
            await _mongoService.UpdateReport(id);

        }
    }
}
