using PADLab2_1part.Data;
using PADLab2_1part.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PADLab2_1part.Services
{
    public class LikeService : ILikeService
    {
        private IUserRepo userRepo;
        private ILikesRepo likeRepo;

        public LikeService(ILikesRepo _likeRepo, IUserRepo _userRepo)
        {
            likeRepo = _likeRepo;
            userRepo = _userRepo;
        }

        public async Task AddLike(Like like)
        {
            await userRepo.GetUserById(like.UserId);
            await likeRepo.AddLike(like);
        }

        public async Task DeleteLike(Like like)
        {
            await likeRepo.DeleteLike(like);
        }

        public async Task<IEnumerable<Guid>> GetLikesUsers(Guid id)
        {
           return await likeRepo.GetLikesUsers(id);
             
        }

        public async Task<LikeCount> GetNumberOfLikes(Guid id)
        {
           return await likeRepo.GetNumberOfLikes(id);
        }
    }
}
