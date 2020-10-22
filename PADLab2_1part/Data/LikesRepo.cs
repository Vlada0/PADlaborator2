using MongoDB.Driver;
using PADLab2_1part.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PADLab2_1part.Data
{
    public class LikesRepo : ILikesRepo
    {
        private IMongoCollection<Like> collectionLikes;

        public LikesRepo(IMongoClient client)
        {
            var database = client.GetDatabase("PicturesDB");
            collectionLikes = database.GetCollection<Like>("Likes");
        }
        public void AddLike(Like like)
        {
            collectionLikes.InsertOne(like);
        }

        public void DeleteLike(Like like)
        {
            collectionLikes.DeleteOne(_like=>_like.ImageId==like.ImageId&&_like.UserId==like.UserId);
        }

        public IEnumerable<string> GetLikesUsers(string id)
        {
            return collectionLikes.Find(s=>s.ImageId==id).ToEnumerable().Select(s=>s.UserId);
        }

        public int GetNumberOfLikes(string id)
        {
            return (int)collectionLikes.Count(s => s.ImageId == id);
        }
    }
}
