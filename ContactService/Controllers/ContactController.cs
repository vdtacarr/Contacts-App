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

        [HttpPost()]
        [Route("add-person")]
        public async Task<IActionResult> AddContact([FromBody] ContactDto contact)
        {
           await _mongoService.CreateAsync(contact);
            return Ok();
        }
        [HttpGet()]
        [Route("get-contacts")]
        public async Task<List<Contact>> GetAllContacts()
        {
            return await _mongoService.GetAsync();
        }

    }
}
