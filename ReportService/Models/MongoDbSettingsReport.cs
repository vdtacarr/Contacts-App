using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Services.Concrete
{

    public class MongoDbSettingsReport
    {
        public string ConnectionURI { get; set; }
        public string DatabaseName { get; set; }
        public string ContactCollectionName { get; set; }
        public string ReportCollectionName{ get; set; }

    }

}
