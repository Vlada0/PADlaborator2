using MongoDB.Bson;
using MongoDB.Driver;
using PADLab2_1part.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public Picture CreatePicture(Picture picture)
        {
            collectionPicture.InsertOne(picture);
            return picture;
        }

        public void Delete(string id)
        {
            collectionPicture.DeleteOne(picture => picture.Id==id);
        }

        public Picture GetPictureById(string id)
        {
            return collectionPicture.Find(picture => picture.Id == id).FirstOrDefault();
        }

        public IEnumerable<Picture> GetPictures()
        {
            return collectionPicture.Find(new BsonDocument()).ToEnumerable();
        }

        public Picture Update(Picture picture)
        {
            collectionPicture.ReplaceOne(_picture => _picture.Id == picture.Id, picture);
            return picture;
        }
    }
}
