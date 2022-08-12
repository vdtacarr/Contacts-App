using AutoMapper;
using Shared.Models;
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
    public class ContactMongoService
    {
        private readonly IMongoCollection<Contact> _contactCollection;
        private readonly IMapper _mapper;

        public ContactMongoService(IOptions<MongoDbSettingsContact> mongoDBSettings, IMapper mapper)
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
            try
            {
                return await _contactCollection.Find(filter).FirstAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task CreateContactAsync(ContactDto contactDto)
        {
            
            Contact contact = _mapper.Map<Contact>(contactDto);
            if(contact == null)
            {
                throw new ArgumentNullException();
            }
            await _contactCollection
                .InsertOneAsync(contact);
        }
        public async Task UpdateContactAsync(string id, ContactInfoDto info)
        {
            ContactInfo contactInfo = _mapper.Map<ContactInfo>(info);
            contactInfo.ContactInfoId = ObjectId.GenerateNewId().ToString();
            if (string.IsNullOrEmpty(id) || info == null)
            {
                throw new ArgumentNullException();
            }
            FilterDefinition<Contact> filter = Builders<Contact>.Filter.Eq("Id", id);
            UpdateDefinition<Contact> update = Builders<Contact>.Update.AddToSet<ContactInfo>("ContactInfo", contactInfo);
            await _contactCollection.UpdateOneAsync(filter, update);
        }
        public async Task DeleteContactAsync(string id)
        {
            FilterDefinition<Contact> filter = Builders<Contact>.Filter.Eq("Id", id);
            try
            {
                await _contactCollection.DeleteOneAsync(filter);
            }
            catch
            {
                throw new ArgumentNullException();
            }

        }
        public async Task DeleteContactInfoAsync(string contactId, string contactInfoId)
        {
            FilterDefinition<Contact> filter = Builders<Contact>.Filter.Eq("Id", contactId);
            var update = Builders<Contact>.Update.PullFilter("ContactInfo",
                Builders<ContactInfo>.Filter.Eq("ContactInfoId", contactInfoId));
            try
            {
                await _contactCollection.FindOneAndUpdateAsync(filter, update);
            }
            catch
            {
                throw new ArgumentNullException();
            }
        }

        

    }
}
