using PADLab2_1part.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PADLab2_1part.Data
{
    public interface IPictureRepo
    {
        Task<IEnumerable<Picture>> GetPictures();
        Task<Picture> GetPictureById(Guid id);
        Task<Picture> CreatePicture(Picture picture);
        Task<Picture> Update(Picture picture);
        Task Delete(Guid id);
    }
}
