using MassTransit;
using ProcessService.Services;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessService.Customer
{
    public class ReportPrepearedEventConsumer : IConsumer<ReportCreatedEvent>
    {
        readonly MongoService _mongoService;
        public ReportPrepearedEventConsumer(MongoService mongoService)
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
