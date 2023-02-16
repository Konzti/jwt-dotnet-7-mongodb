using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using JwtDotNet7.Models;
using JwtDotNet7.Services.Interfaces;
using JwtDotNet7.Settings.MongoDB;

namespace JwtDotNet7.Services
{
    public class UserService : IUserService
    {

        private readonly IMongoCollection<User> _Users;
        public UserService(IMongoDBSettings settings, MongoClientBase client)
        {
            _Users = client.GetDatabase(settings.DatabaseName).GetCollection<User>(settings.CollectionName);
            Console.WriteLine("Successfully connected to MongoDB");

        }
        public User GetUserById(string id)
        {
            return _Users.Find(User => User.Id == id).FirstOrDefault();
        }


        public List<User> GetUsers()
        {
            var sort = Builders<User>.Sort.Descending(c => c.CreatedAt);
            return _Users.Find(User => true).Sort(sort).ToList();
        }
    }

}