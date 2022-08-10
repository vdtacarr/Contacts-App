using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Events
{
    public class ReportCreatedEvent :IEvent
    {
        public string Id { get; set; }
    }
    public interface IEvent
    {

    }
}
