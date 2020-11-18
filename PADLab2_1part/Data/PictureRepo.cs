using MongoDB.Bson;
using MongoDB.Driver;
using PADLab2_1part.Models;
using PADLab2_1part.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace PADLab2_1part.Data
{
    public class PictureRepo : IPictureRepo
    {
        private IMongoCollection<Picture> collectionPicture;

        public PictureRepo(IMongoClient client)
        {
            var database = client.GetDatabase("PicturesDB");
            collectionPicture = database.GetCollection<Picture>("Pictures");
        }
        public async Task<Picture> CreatePicture(Picture picture)
        {
            await collectionPicture.InsertOneAsync(picture);
            return picture;
        }

        public async Task Delete(Guid id)
        {
            var result = await collectionPicture.DeleteOneAsync(picture => picture.Id==id);
            if (result.DeletedCount == 0)
            {
                throw new NotFoundException($"Picture with id {id} not found!");
            }

        }

        public async Task<Picture> GetPictureById(Guid id)
        {
            var picture = await collectionPicture.FindAsync<Picture>(picture => picture.Id == id);
            var pic = await picture.FirstOrDefaultAsync();
            if(pic == null)
            {
                throw new NotFoundException("Picture not found!");
            }
            return pic;
        }

        public async Task<IEnumerable<Picture>> GetPictures()
        {
            var pictures = await collectionPicture.FindAsync(new BsonDocument());
            return pictures.ToEnumerable();
        }

        public async Task<Picture> Update(Picture picture)
        {
            var pictureToUpdate = await collectionPicture.FindAsync(s => s.Id == picture.Id);
            var pictureToUpdateValue = pictureToUpdate.FirstOrDefault();
            if (pictureToUpdateValue == null)
            {
                throw new NotFoundException("No picture with such Id");
            }
            if(pictureToUpdateValue.Author != picture.Author)
            {
                throw new BadRequestException("You cannot change author Id");
            }

            await collectionPicture.ReplaceOneAsync(_picture => _picture.Id == picture.Id, picture);
            
            return picture;
        }
    }
}
