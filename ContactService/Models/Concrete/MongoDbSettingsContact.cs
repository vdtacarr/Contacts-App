using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Models.Concrete
{
    public class MongoDbSettingsContact
    {
        public string ConnectionURI { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

    }
}
