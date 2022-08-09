using AutoMapper;
using Common.Models;
using ContactService.Model;
using ContactService.Models.Concrete;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Services
{
    public class MongoService
    {
        private readonly IMongoCollection<Contact> _contactCollection;
        private readonly IMapper _mapper;

        public MongoService(IOptions<MongoDbSettings> mongoDBSettings, IMapper mapper)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _contactCollection = database.GetCollection<Contact>(mongoDBSettings.Value.CollectionName);
            _mapper = mapper;
        }
        public async Task<List<Contact>> GetContactsAsync()
        {
            return await _contactCollection
                .Find(new BsonDocument())
                .ToListAsync();
        }
        public async Task<Contact> GetContactByIdAsync(string id)
        {
            FilterDefinition<Contact> filter = Builders<Contact>.Filter.Eq("Id", id);
            return  await _contactCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task CreateContactAsync(ContactDto contactDto)
        {
            Contact contact = _mapper.Map<Contact>(contactDto);

            await _contactCollection
                .InsertOneAsync(contact);
        }
        public async Task UpdateContactAsync(string id, ContactInfo info)
        {
            FilterDefinition<Contact> filter = Builders<Contact>.Filter.Eq("Id", id);
            UpdateDefinition<Contact> update = Builders<Contact>.Update.Set<ContactInfo>("ContactInfo", info);
            await _contactCollection.UpdateOneAsync(filter, update);
        }
        public async Task DeleteContactAsync(string id)
        {
            FilterDefinition<Contact> filter = Builders<Contact>.Filter.Eq("Id", id);
            await _contactCollection.DeleteOneAsync(filter);

        }
        public async Task DeleteContactInfoAsync(string id)
        {
            FilterDefinition<Contact> filter = Builders<Contact>.Filter.Eq("Id", id);
            UpdateDefinition<Contact> update = Builders<Contact>.Update.Set<ContactInfo>("ContactInfo", null);
            await _contactCollection.UpdateOneAsync(filter, update);

        }

        

    }
}
