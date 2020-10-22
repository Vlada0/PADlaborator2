using PADLab2_1part.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PADLab2_1part.Data
{
    public interface IPictureRepo
    {
        IEnumerable<Picture> GetPictures();
        Picture GetPictureById(string id);
        Picture CreatePicture(Picture picture);
        Picture Update(Picture picture);
        void Delete(string id);
    }
}
