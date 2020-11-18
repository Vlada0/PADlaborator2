using MongoDB.Driver;
using PADLab2_1part.Models;
using PADLab2_1part.Validation;
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
        public async Task AddLike(Like like)
        {
            var likeCursor = await collectionLikes.FindAsync(_like => _like.ImageId == like.ImageId && _like.UserId == like.UserId);
            var _like = await likeCursor.FirstOrDefaultAsync();
            if(_like != null)
            {
                throw new ResourseAlreadyExistException("This user already liked this image");
            }
            like.Date = DateTime.Now.ToString();
            await collectionLikes.InsertOneAsync(like);
        }

        public async Task DeleteLike(Like like)
        {
            var result = await collectionLikes.DeleteOneAsync(_like => _like.ImageId == like.ImageId && _like.UserId == like.UserId);
            if (result.DeletedCount == 0)
            {
                throw new NotFoundException("Like not found!");
            }
            
        }

        public async Task<IEnumerable<Guid>> GetLikesUsers(Guid id)
        {
            var likesUser = await collectionLikes.FindAsync(s => s.ImageId == id);
            return likesUser.ToEnumerable().Select(s=>s.UserId.Value);
        }

        public async Task<LikeCount> GetNumberOfLikes(Guid id)
        {
            var count = await collectionLikes.CountDocumentsAsync(s => s.ImageId == id);
            var count1 = (int)count;
            return new LikeCount(count1);
        }
    }
}
