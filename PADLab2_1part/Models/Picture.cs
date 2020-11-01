using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PADLab2_1part.Models
{
    public class Picture
    {
       [BsonId]
       // [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
       // [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
        [BsonElement("name")]
        [Required]
        public string Name { get; set; }
        [Required]
        [BsonElement("image")]
        public string Image { get; set; }
        [BsonElement("authorId")]
        [Required]
        public Guid? Author { get; set; }
        [BsonElement("description")]
        [Required]
        public string Description { get; set; }

        public void Copy(Picture picture)
        {
            Name = picture.Name;
            Image = picture.Image;
            Author = picture.Author;
            Description = picture.Description;
        }

    }
}
