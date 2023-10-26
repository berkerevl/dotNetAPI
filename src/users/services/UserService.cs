using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using DotNetAPI.Users.Entities;
using DotNetAPI.Users.Dtos;

namespace DotNetAPI.Users.Services
{
    public class UsersService
    {
        private readonly IMongoCollection<User> _users;

        public UsersService(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("Users");
        }

        public async Task<User> AddUser(CreateUserDto createUserDto)
        {
            var user = new User(
                createUserDto.FirstName,
                createUserDto.LastName,
                createUserDto.Email,
                createUserDto.Password,
                createUserDto.TotalWealth
            );

            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task<User> GetUserById(string userId)
        {
            return await _users.Find(u => u.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task UpdateUser(UpdateUserDto updateUserDto)
        {
            var filter = Builders<User>.Filter.Eq(u => u.UserId, updateUserDto.UserId);
            var update = Builders<User>.Update
                .Set(u => u.FirstName, updateUserDto.FirstName)
                .Set(u => u.LastName, updateUserDto.LastName)
                .Set(u => u.Email, updateUserDto.Email)
                .Set(u => u.Password, User.HashPassword(updateUserDto.Password))
                .Set(u => u.PhoneNumber, updateUserDto.PhoneNumber)
                .Set(u => u.NickName, updateUserDto.NickName)
                .Set(u => u.UpdatedAt, DateTime.UtcNow);

            await _users.UpdateOneAsync(filter, update);
        }

        public async Task SoftDeleteUser(string userId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.UserId, userId);
            var update = Builders<User>.Update.Set(u => u.DeletedAt, DateTime.UtcNow);

            await _users.UpdateOneAsync(filter, update);
        }

        public async Task<double> GetUserMoney(string userId)
        {
            var user = await GetUserById(userId);
            return user?.TotalWealth ?? 0;
        }
    }
}
