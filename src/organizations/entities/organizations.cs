using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetAPI.Organizations.Entities
{
    public class Organization
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;  // Initialize Id with an empty string.

        [BsonElement("OrganizationName")]
        [BsonRequired]
        public string OrganizationName { get; set; }

        [BsonElement("BannedList")]
        public string[]? BannedList { get; set; }  // BannedList is nullable.

        [BsonElement("CreatedAt")]
        [BsonRequired]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("UpdatedAt")]
        [BsonRequired]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("DeletedAt")]
        public DateTime? DeletedAt { get; set; }  // DeletedAt is nullable.

        [BsonElement("ApiKey")]
        [BsonRepresentation(BsonType.String)]
        [BsonRequired]
        public Guid ApiKey { get; set; } = Guid.NewGuid();

        public Organization(string organizationName)
        {
            OrganizationName = organizationName ?? throw new ArgumentNullException(nameof(organizationName));
        }
    }
}
