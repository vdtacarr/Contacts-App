using AutoMapper;
using Shared.Models;
using ContactService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactService.Models.Concrete;

namespace ContactService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactDto>();
            CreateMap<ContactDto, Contact>();
            CreateMap<ContactInfo, ContactInfoDto>();
            CreateMap<ContactInfoDto, ContactInfo>();
        }
        
    }
}
