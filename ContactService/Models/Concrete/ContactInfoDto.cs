using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Models.Concrete
{
    public class ContactInfoDto
    {
        public string TelNo { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string InfoDetail { get; set; }
    }
}
