using PADLab2_1part.Data;
using PADLab2_1part.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

namespace PADLab2_1part.Services
{
    public class LikeService : ILikeService
    {
        private IUserRepo userRepo;
        private ILikesRepo likeRepo;
        private IPictureRepo pictureRepo;

        public LikeService(ILikesRepo _likeRepo, IUserRepo _userRepo, IPictureRepo _pictureRepo)
        {
            likeRepo = _likeRepo;
            userRepo = _userRepo;
            pictureRepo = _pictureRepo;
        }

        public async Task AddLike(Like like)
        {
            await pictureRepo.GetPictureById(like.ImageId.Value);
            await userRepo.GetUserById(like.UserId.Value);
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
