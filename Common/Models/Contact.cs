using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Shared.Models
{
    public class Contact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string UserName { get; set; }
        public string UserSurname{ get; set; }
        public string Company { get; set; }
        public ContactInfo ContactInfo { get; set; }


    }
}
