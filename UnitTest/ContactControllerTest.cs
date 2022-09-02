using AutoMapper;
using ContactService.Controllers;
using ContactService.Mapping;
using ContactService.Model;
using ContactService.Models.Concrete;
using ContactService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReportService.Services;
using ReportService.Services.Concrete;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class ContactControllerTest
    {
        private readonly IMongoCollection<Contact> _contactCollection;
        private readonly IMongoCollection<Report> _reportCollection;
        private readonly IMapper _mapper;
        private readonly ContactController contactController;
        public ContactControllerTest()
        {
            var mongoDBSettingsContact = Options.Create(new MongoDbSettingsContact
            {
                ConnectionURI =
                "mongodb+srv://vedatacar:12asd34ert@vedatacar.biput.mongodb.net/?retryWrites=true&w=majority",
                DatabaseName = "SeturTest",
                CollectionName = "ContactTestDB"
            });
            var mongoDBSettingsReport = Options.Create(new MongoDbSettingsReport
            {
                ConnectionURI =
                "mongodb+srv://vedatacar:12asd34ert@vedatacar.biput.mongodb.net/?retryWrites=true&w=majority",
                DatabaseName = "SeturTest",
                ContactCollectionName = "ContactTestDB",
                ReportCollectionName = "ReportTestDB"
            });

            MapperConfiguration mapperConfig = new MapperConfiguration(
         cfg =>
         {
             cfg.AddProfile(new MappingProfile());
         });

            IMapper mapper = new Mapper(mapperConfig);
            var contactMongoService = new ContactMongoService(mongoDBSettingsContact, mapper);
            var reportMongoService = new ReportMongoService(mongoDBSettingsReport);
            contactController = new ContactController(contactMongoService);
        }

        [Fact]
        public async Task AddContact_When_ContactDto_IsNotNull_Return_OkObjectResult()
        {
            var contactDto = new ContactDto()
            {
                Company = "abc",
                UserName = "ahmet",
                UserSurname = "Mehmet"
            };

            var res = await contactController.AddContact(contactDto);
            Assert.IsType<OkObjectResult>(res);
        }
        [Fact]
        public async Task AddContact_When_ContactDto_Null_Return_BadRequestObject_Result()
        {
            ContactDto contactDto = null;

            var res = await contactController.AddContact(contactDto);
            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public async Task UpdateContact_When_ContactInfo_IsNotNull_Id_Null_Return_BadRequestObjectResult()
        {
            string id = null;
            ContactInfoDto contactInfoDto = new ContactInfoDto { };

            var res = await contactController.AddContactInfo(id, contactInfoDto);
            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public async Task UpdateContact_When_Id_Not_Guid_Return_BadRequestObjectResult()
        {
            string id = "1234";
            ContactInfoDto contactInfoDto = new ContactInfoDto { };

            var res = await contactController.AddContactInfo(id, contactInfoDto);
            Assert.IsType<BadRequestObjectResult>(res);
        }
        [Fact]
        public async Task UpdateContact_When_Id_Guid_ContactInfo_Not_Null_Return_OkObjectResult()
        {
            string id = "62f6a5e254634c7e17436ec8";
            ContactInfoDto contactInfoDto = new ContactInfoDto { 
             Email = "string",
             TelNo = "string"
            };
            var res = await contactController.AddContactInfo(id, contactInfoDto);
            Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async Task GetAllContacts_When_Has_Records_Return_Compatible_Type()
        {
            var res = await contactController.GetAllContacts();

            Assert.IsType<ActionResult<List<Contact>>>(res);
        }
        [Fact]
        public async Task GetAllContacts_When_Has_Records_Record_Number_Greater_Than_Zero()
        {
            Assert.ThrowsAsync<ArgumentException>(() => contactController.GetAllContacts());
            
        }

        [Fact]
        public async Task GetAllContacts_When_Has_No_Records_Return_String_OkObjectResult()
        {
            var res = await contactController.GetAllContacts();
            if (res.Value.Any())
            {
                Assert.IsType<ActionResult<List<Contact>>>(res);
            }
            else
            {
                Assert.IsType<ActionResult<string>>(res);
            }
        }
        [Fact]
        public async Task GetContactById_When_Id_Is_Invalid_Return_BadObjectResult()
        {
            string id = "1234";
            Assert.Throws<InvalidOperationException>(() => contactController.GetContactByIdAsync(id).Result);
        }

        [Fact]
        public async Task GetContactById_When_Id_Is_Invalid_Return_OkObjectResult()
        {
            string id = "62f6a5e254634c7e17436ec8";
            var res = await contactController.GetContactByIdAsync(id);
            Assert.IsType<ActionResult<Contact>>(res);
        }
        [Fact]
        public async Task GetContactById_Control_Returned_RecordNumber()
        {
            string id = "62f6a5e254634c7e17436ec8";
            var res = await contactController.GetContactByIdAsync(id);
            Assert.NotNull(res.Value);
        }
        [Fact]
        public async Task DeleteContact_When_Invalid_Id_Return_BadObjectResult()
        {
            string contactId = "1234";
            var res = await contactController.DeleteContact(contactId);
            Assert.IsType<BadRequestObjectResult>(res);
        }
        [Fact]
        public async Task DeleteContact_When_Valid_Id_Return_OkObjectResult()
        {
            string contactId = "62f6a699a01279cf1d895ee2";
            var res = await contactController.DeleteContact(contactId);
            Assert.IsType<OkObjectResult>(res);
        }
        [Fact]
        public async Task DeleteContactInfo_When_Invalid_Id_Return_BadObjectResult()
        {
            string contactId = "1234";
            string contactInfoId = "1234";
            var res = await contactController.DeleteContactInfo(contactId, contactInfoId);
            Assert.IsType<BadRequestObjectResult>(res);
        }
        [Fact]
        public async Task DeleteContactInfo_When_Valid_Id_Return_OkObjectResult()
        {
            string contactId = "62f6a5e254634c7e17436ec8";
            string contactInfoId = "62f6a77fa85b94ccce08826d";
            var res = await contactController.DeleteContactInfo(contactId, contactInfoId);
            Assert.IsType<OkObjectResult>(res);
        }

    }
}
