using MongoDB.Bson;
using MongoDB.Driver;
using PADLab2_1part.Models;
using PADLab2_1part.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PADLab2_1part.Data
{
    public class UserRepo: IUserRepo
    {
        private IMongoCollection<User> collectionUser;

        public UserRepo(IMongoClient client)
        {
            var database = client.GetDatabase("PicturesDB");
            collectionUser = database.GetCollection<User>("Users");
        }

        public async Task<User> CreateUser(User user)
        {
            await collectionUser.InsertOneAsync(user);
            return user;
        }

        public async Task DeleteUser(Guid id)
        {
            var result = await collectionUser.DeleteOneAsync(user => user.UserId == id);
            if (result.DeletedCount == 0)
            {
                throw new NotFoundException($"User with UserId {id} not found!");
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await collectionUser.FindAsync<User>(user => user.UserId == id);
            var us = await user.FirstOrDefaultAsync();
            if (us == null)
            {
                throw new NotFoundException("User not found!");
            }
            return us;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await collectionUser.FindAsync(new BsonDocument());
            return users.ToEnumerable();
        }

        public async Task<User> UpdateUser(User user)
        {
            var result = await collectionUser.ReplaceOneAsync(_user => _user.UserId == user.UserId, user);
            if (result.MatchedCount== 0)
            {
                throw new NotFoundException("No user with such Id");
            }
            return user;
        }
    }
}
