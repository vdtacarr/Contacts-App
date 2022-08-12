using MassTransit;
using ProcessService;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessService.Customer
{
    public class ReportPrepearedEventConsumer : IConsumer<ReportCreatedEvent>
    {
        readonly ProcessMongoService _mongoService;
        public ReportPrepearedEventConsumer(ProcessMongoService mongoService)
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
