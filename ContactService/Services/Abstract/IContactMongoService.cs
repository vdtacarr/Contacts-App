using ContactService.Model;
using ContactService.Models.Concrete;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Services
{
    public interface IContactMongoService 
    {
        Task<List<Contact>> GetContactsAsync();
        Task<Contact> GetContactByIdAsync(string id);
        Task CreateContactAsync(ContactDto contactDto);
        Task UpdateContactAsync(string id, ContactInfoDto info);
        Task DeleteContactAsync(string id);
        Task DeleteContactInfoAsync(string contactId, string contactInfoId);
    }
}
