using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace softlocke_server
{
    public class ProjectItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("Name")]
        public string? Name { get; set; }

        [BsonElement("Paragraphs")]
        public string[]? Paragraphs { get; set; }

        [BsonElement("Blurb")]
        public string? Blurb { get; set; }

        [BsonElement("LinkPaths")]
        public string[]? LinkPaths { get; set; }

        [BsonElement("LinkNames")]
        public string[]? LinkNames { get; set; }

        [BsonElement("Command")]
        public string? Command { get; set; }

        [BsonElement("Date")]
        public string? Date { get; set; }

        [BsonElement("Admin")]
        public string? Admin { get; set; }

        [BsonElement("AdminId")]
        public string? AdminId { get; set; }

        [BsonElement("Images")]
        public string[]? Images { get; set; }
    }
}