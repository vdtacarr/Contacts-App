using Shared.Models;
using ContactService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactService.Services;
using ContactService.Models.Concrete;

namespace ContactService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ContactMongoService _contactService;

        public ContactController(ContactMongoService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        [Route("add-contact")]
        public async Task<IActionResult> AddContact([FromBody] ContactDto contact)
        {
            try
            {
                await _contactService.CreateContactAsync(contact);
                return Ok("Başarılı bir şekilde kaydedildi");
            }

            catch (Exception ex)
            {
                return BadRequest($"Beklenmeyen bir hata oluştu : {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get-contacts")]
        public async Task<ActionResult<List<Contact>>> GetAllContacts()
        {
            try
            {
                var res = await _contactService.GetContactsAsync();
                if(res.Count == 0)
                {
                    return Ok("Hiç Kayıt Yok!!");
                }
                else
                {
                    return Ok(res);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Beklenmeyen bir hata oluştu : {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get-contact-by-id/{id}")]
        public async Task<ActionResult<Contact>> GetContactByIdAsync(string id)
        {
            try
            {
                var res = await _contactService.GetContactByIdAsync(id);
                if(res != null)
                {
                    return Ok(res);
                }
                else
                {
                    return BadRequest("Bu Idli kayıt yok!!");
                }
            
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Beklenmeyen bir hata oluştu : {ex.Message}");
            }
        }

        [HttpPost]
        [Route("add-contact-info/{id}")]
        public async Task<IActionResult> AddContactInfo(string id, [FromBody] ContactInfoDto info)
        {
            try
            {
                await _contactService.UpdateContactAsync(id, info);
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
                await _contactService.DeleteContactAsync(id);
                return Ok($"{id} idli kayıt başarıyla silindi");
            }
            catch (Exception ex)
            {
                return BadRequest($"Bu idli kayıt yok: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete-contact-info/{contactId}/{contactInfoId}")]
        public async Task<IActionResult> DeleteContactInfo(string contactId, string contactInfoId)
        {
            try
            {
                await _contactService.DeleteContactInfoAsync(contactId, contactInfoId);
                return Ok($"{contactId} idli kaydın kontak bilgisi başarılı bir şekilde kaldırıldı.");

            }
            catch (Exception ex)
            {
                return BadRequest( $"Bu idli kayıt yok: {ex.Message}");

            }
        }

        
    }
}
