using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace CsvTask.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Range(1, 150, ErrorMessage = "Only positive number allowed between 1 and 150")]
        public int Age { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
