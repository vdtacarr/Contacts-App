using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status{ get; set; }
        public List<ReportInfo> ReportInfo { get; set; }
    }
    
}
