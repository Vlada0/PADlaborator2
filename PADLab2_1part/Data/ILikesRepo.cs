using PADLab2_1part.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PADLab2_1part.Data
{
    public interface ILikesRepo
    {
        IEnumerable<string> GetLikesUsers(string id);
        int GetNumberOfLikes(string id);
        void DeleteLike(Like like);
        void AddLike(Like like);
    }
}
