using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetAPI.Players.Entities
{
    public class Character
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Health")]
        public int Health { get; set; }
    }
}
