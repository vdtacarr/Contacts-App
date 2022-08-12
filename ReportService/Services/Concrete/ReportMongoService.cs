using MassTransit;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Shared.Events;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Services.Concrete
{
    public class ReportMongoService
    {
        private readonly IMongoCollection<Report> _reportCollection;
        public ReportMongoService(IOptions<MongoDbSettingsReport> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _reportCollection = database.GetCollection<Report>(mongoDBSettings.Value.ReportCollectionName);
        }

        public async Task<ReportCreatedEvent> CreateReportAsync()
        {
            
            #region Report Creation
            Report rapor = new Report
            {
                CreatedDate = DateTime.Now,
                Status = "Hazırlanıyor",
                ReportInfo = null
            };
            #endregion

            await _reportCollection.InsertOneAsync(rapor);

            ReportCreatedEvent reportCreatedEvent = new ReportCreatedEvent()
            {
                Id = rapor.Id
            };
            return reportCreatedEvent;

        }

        public async Task<List<Report>> GetAllReports()
        {
            return await _reportCollection.Find(new BsonDocument()).ToListAsync();
            
        }
        public async Task<Report> GetReportByIdAsync(string id)
        {
            FilterDefinition<Report> filter = Builders<Report>.Filter.Eq("Id", id);
            return await _reportCollection.Find(filter).FirstAsync();
        }
    }
}
