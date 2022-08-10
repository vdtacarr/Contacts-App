using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportService.Services.Concrete;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly MongoService _mongoService;
        readonly IPublishEndpoint _publishEndpoint;

        public ReportController(MongoService mongoService, IPublishEndpoint publishEndpoint)
        {
            _mongoService = mongoService;
            _publishEndpoint = publishEndpoint;
        }
        
        [HttpGet]
        [Route("add-report")]
        public async Task<IActionResult> AddReport()
        {
            try
            {
                var reportCreatedEvent = await _mongoService.CreateReportAsync();
                await _publishEndpoint.Publish(reportCreatedEvent);
                return Ok("Başarılı bir şekilde rapor oluşturuldu.");

            }
            catch(Exception ex)
            {
                return BadRequest("Bilinmeyen bir hata oluştu.");
            }
        }
        [HttpGet]
        [Route("get-all-reports")]
        public async Task<List<Report>> GetAllReports()
        {
            return await _mongoService.GetAllReports();
        }
        [HttpGet]
        [Route("get-one-report/{id}")]
        public async Task<Report> GetOneReport(string id)
        {
            return await _mongoService.GetReportByIdAsync(id); 
        }
    }
}
