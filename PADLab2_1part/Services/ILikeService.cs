using PADLab2_1part.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PADLab2_1part.Services
{
    public interface ILikeService
    {
        Task<IEnumerable<Guid>> GetLikesUsers(Guid id);
        Task<LikeCount> GetNumberOfLikes(Guid id);
        Task DeleteLike(Like like);
        Task AddLike(Like like);
    }
}
