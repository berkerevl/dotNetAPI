using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using BCrypt.Net;

namespace DotNetAPI.Users.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string UserId { get; set; } = Guid.NewGuid().ToString();

        [BsonElement("FirstName")]
        [BsonRequired]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        [BsonRequired]
        public string LastName { get; set; }

        [BsonElement("Email")]
        [BsonRequired]
        public string Email { get; set; }

        [BsonElement("Phone")]
        public string? Phone { get; set; }

        [BsonElement("Password")]
        [BsonRequired]
        public string Password { get; private set; }

        [BsonElement("TotalWealth")]
        public double TotalWealth { get; set; }

        public User(string firstName, string lastName, string email, string password, double totalWealth)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = HashPassword(password ?? throw new ArgumentNullException(nameof(password)));
            TotalWealth = totalWealth;
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool ValidatePassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }
    }
}
