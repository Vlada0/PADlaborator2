using PADLab2_1part.Data;
using PADLab2_1part.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PADLab2_1part.Services
{
    public class PictureService : IPictureService
    {
        private IUserRepo userRepo;
        private IPictureRepo pictureRepo;

        public PictureService(IPictureRepo _pictureRepo, IUserRepo _userRepo)
        {
            pictureRepo = _pictureRepo;
            userRepo = _userRepo;
        }

        public async Task<Picture> CreatePicture(Picture picture)
        {
            await userRepo.GetUserById(picture.Author.Value);
            return await pictureRepo.CreatePicture(picture);
        }

        public async Task Delete(Guid id)
        {
            await pictureRepo.Delete(id);
        }

        public async Task<Picture> GetPictureById(Guid id)
        {
            return await pictureRepo.GetPictureById(id);
        }

        public async Task<IEnumerable<Picture>> GetPictures()
        {
            return await pictureRepo.GetPictures();
        }

        public async Task<Picture> Update(Picture picture)
        {
            return await pictureRepo.Update(picture);
        }
    }
}
