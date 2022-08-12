using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ProcessService.Models;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessService
{
    public class ProcessMongoService
    {
        private readonly IMongoCollection<Report> _reportCollection;
        private readonly IMongoCollection<Contact> _contactCollection;

        public ProcessMongoService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _reportCollection = database.GetCollection<Report>(mongoDBSettings.Value.ReportCollectionName);
            _contactCollection = database.GetCollection<Contact>(mongoDBSettings.Value.ContactCollectionName);
        }
        public async Task UpdateReport(string Id)
        {
            List<ReportInfo> reportInfo = new List<ReportInfo>();

            List<Contact> contacts = await _contactCollection.Find(new BsonDocument()).ToListAsync();
            var groupedContacts = contacts
                .Where(x => x.ContactInfo.Any())
                  .GroupBy(x => x.ContactInfo.First().Location);

            #region Grouping
            if (groupedContacts.Any())
            {
                foreach (var item in groupedContacts)
                {
                    reportInfo.Add(new ReportInfo
                    {
                        Location = item.Key,
                        PeopleCount = item.Count(),
                        TelNoCount = item.Count()
                    });
                }
            }

            #endregion

            FilterDefinition<Report> filter = Builders<Report>.Filter.Eq("Id", Id);

            UpdateDefinition<Report> update1 = Builders<Report>.Update.Set<List<ReportInfo>>("ReportInfo", reportInfo);
            UpdateDefinition<Report> update2 = Builders<Report>.Update.Set<string>("Status", "Hazırlandı");

            await _reportCollection.UpdateOneAsync(filter, update1);
            await _reportCollection.UpdateOneAsync(filter, update2);
            
        }

    }
}
