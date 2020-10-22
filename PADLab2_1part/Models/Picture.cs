using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PADLab2_1part.Models
{
    public class Picture
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("image")]
        public string Image { get; set; }
        [BsonElement("author")]
        public string Author { get; set; }
        [BsonElement("description")]
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
