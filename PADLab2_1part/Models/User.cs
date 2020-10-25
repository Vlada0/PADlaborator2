using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PADLab2_1part.Models
{
    public class User
    {
        [BsonId]
        public Guid UserId { get; set; }
        [BsonElement("imageUrl")]
        [Required]
        public string ImageURL { get; set; }
        [BsonElement("firstName")]
        [Required]
        public string FirstName { get; set; }
        [BsonElement("secondName")]
        [Required]
        public string SecondName { get; set; }
    }
}
