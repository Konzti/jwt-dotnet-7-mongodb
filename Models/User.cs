using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JwtDotNet7.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        [BsonElement("firstName")]
        public string FirstName { get; set; } = String.Empty;
        [BsonElement("lastName")]
        public string LastName { get; set; } = String.Empty;
        [BsonElement("email")]
        public string Email { get; set; } = String.Empty;
        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; } = String.Empty;
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}