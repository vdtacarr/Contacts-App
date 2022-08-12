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
        private readonly ReportMongoService _mongoService;
        readonly IPublishEndpoint _publishEndpoint;

        public ReportController(ReportMongoService mongoService, IPublishEndpoint publishEndpoint)
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
                return Ok($"{reportCreatedEvent.Id} idli rapor oluşturuluyor...");

            }
            catch(Exception ex)
            {
                return StatusCode(500, "Bilinmeyen bir hata oluştu.");
            }
        }
        [HttpGet]
        [Route("get-all-reports")]
        public async Task<ActionResult<List<Report>>> GetAllReports()
        {
            try
            {
                return Ok(await _mongoService.GetAllReports());

            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Beklenmeyen bir hata oluştu : {ex.Message}");
            }
        }
        [HttpGet]
        [Route("get-one-report/{id}")]
        public async Task<ActionResult<Report>> GetSingleReport(string id)
        {
            try
            {
                return Ok(await _mongoService.GetReportByIdAsync(id));

            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Beklenmeyen bir hata oluştu : {ex.Message}");

            }
        }
    }
}
