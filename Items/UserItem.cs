using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.AspNetCore.Http;

namespace softlocke_server
{
    public class UserItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("Username")]
        public string? Username { get; set; }

        [BsonElement("FirebaseId")]
        public string? FirebaseId { get; set; }

        [BsonElement("Bio")]
        public string? Bio { get; set; }

        [BsonElement("Avatar")]
        public string? Avatar { get; set; }

        [BsonElement("Links")]
        public string[]? Links { get; set; }

        [BsonElement("LinkNames")]
        public string[]? LinkNames { get; set; }

        [BsonElement("Date")]
        public string? Date { get; set; }
    }
}