using Common.Models;
using ContactService.Model;
using ContactService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly MongoService _mongoService;

        public ContactController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpPost]
        [Route("add-person")]
        public async Task<IActionResult> AddContact([FromBody] ContactDto contact)
        {
            try
            {
                await _mongoService.CreateContactAsync(contact);
                return Ok("Başarılı bir şekilde kaydedildi");
            }

            catch (Exception ex)
            {
                return BadRequest($"Beklenmeyen bir hata oluştu : {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get-contacts")]
        public async Task<List<Contact>> GetAllContacts()
        {
            return await _mongoService.GetContactsAsync();
        }

        [HttpGet]
        [Route("get-contact-by-id/{id}")]
        public async Task<Contact> GetContactByIdAsync(string id)
        {
            try
            {
                var res = await _mongoService.GetContactByIdAsync(id);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        [Route("add-contact-info/{id}")]
        public async Task<IActionResult> AddContactInfo(string id, [FromBody] ContactInfo info)
        {
            try
            {
                await _mongoService.UpdateContactAsync(id, info);
                return Ok("Kontak bilgisi başarılı bir şekilde eklendi.");
            }

            catch (Exception ex)
            {
                return BadRequest($"Beklenmeyen bir hata oluştu : {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete-contact/{id}")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            try
            {
                await _mongoService.DeleteContactAsync(id);
                return Ok($"{id} idli kayıt başarıyla silindi");
            }
            catch (Exception ex)
            {
                return BadRequest($"Beklenmeyen bir hata oluştu : {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete-contact-info/{id}")]
        public async Task<IActionResult> DeleteContactInfo(string id)
        {
            try
            {
                await _mongoService.DeleteContactInfoAsync(id);
                return Ok($"{id} idli kaydın kontak bilgisi başarılı bir şekilde kaldırıldı.");

            }
            catch (Exception ex)
            {
                return BadRequest($"Beklenmeyen bir hata oluştu : {ex.Message}");

            }
        }

        
    }
}
