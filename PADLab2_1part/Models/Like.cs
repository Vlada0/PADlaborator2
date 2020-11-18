using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PADLab2_1part.Models
{
    public class Like
    {
        [BsonId]
        //[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        //[BsonRepresentation(BsonType.ObjectId)]
        public Guid LikeId { get; set; }

        [BsonElement("userId")]
        [Required]
        public Guid? UserId { get; set; }

        [BsonElement("imageId")]
        [Required]
        public Guid? ImageId { get; set; }
        
        [BsonElement("date")]
        //[Required]
       // [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public string Date { get; set; }
    }
}
