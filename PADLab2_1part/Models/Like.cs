using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PADLab2_1part.Models
{
    public class Like
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public String LikeId { get; set; }

        [BsonElement("userId")]
        public String UserId { get; set; }
        [BsonElement("imageId")]
        public string ImageId { get; set; }
    }
}
