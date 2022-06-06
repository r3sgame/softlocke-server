using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.AspNetCore.Http;

namespace softlocke_server
{
    public class LogItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("Name")]
        public string? Name { get; set; }

        [BsonElement("Content")]
        public string? Content { get; set; }

        [BsonElement("Poster")]
        public string? Poster { get; set; }

        [BsonElement("PosterId")]
        public string? PosterId { get; set; }

        [BsonElement("Topic")]
        public string? Topic { get; set; }

        [BsonElement("Date")]
        public string? Date { get; set; }

        [BsonElement("Tags")]
        public string? Tags { get; set; }

        [BsonElement("Images")]
        public string[]? Images { get; set; }

        [BsonElement("Snippets")]
        public string[]? Snippets { get; set; }

        [BsonElement("SnippetNames")]
        public string[]? SnippetNames { get; set; }

        [BsonElement("Links")]
        public string[]? Links { get; set; }

        [BsonElement("LinkNames")]
        public string[]? LinkNames { get; set; }
    }
}