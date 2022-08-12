using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models
{
    public class ContactInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ContactInfoId { get; set; }
        public string TelNo { get; set; }
        public string Email{ get; set; }
        public string Location{ get; set; }
        public string InfoDetail { get; set; }
    }
}
