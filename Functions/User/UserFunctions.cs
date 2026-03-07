using Microsoft.EntityFrameworkCore;
using OpticalServer.Models;

namespace OpticalServer.Functions
{
    public class UserFunctions
    {
        private readonly DatabaseContext _db;

        public UserFunctions(DatabaseContext db) => _db = db;

        public async Task<User> CreateUser(UserDTO userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                PasswordHash = userDto.PasswordHash
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }
        public async Task<User> AuthenticateUser(UserDTO userDto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == userDto.UserName);

            if (user == null || user.PasswordHash != userDto.PasswordHash)
            {
                RuntimeFunctions.Request($"Failed authentication attempt for user {userDto.UserName}");
                return null;
            }

            RuntimeFunctions.Request($"User {user.UserName} authenticated successfully");

            return user;
        }
        public async Task<User> GetUserById(long userId)
        {
            RuntimeFunctions.Request($"Fetching user with ID {userId}");
            return await _db.Users.FindAsync(userId);
        }
        public async Task<User> GetUserByUsername(string username)
        {
            RuntimeFunctions.Request($"Fetching user with username {username}");
            return await _db.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
        public async Task<User> ChangeUserName(long userId, string newUsername)
        {
            var user = await _db.Users.FindAsync(userId);

            if (user == null) return null;

            user.UserName = newUsername;
            await _db.SaveChangesAsync();

            RuntimeFunctions.Request($"Changed username of user {userId} to {newUsername}");

            return user;
        }
        public async Task<User> ChangePassword(long userId, string newPasswordHash)
        {
            var user = await _db.Users.FindAsync(userId);

            if (user == null) return null;

            user.PasswordHash = newPasswordHash;
            await _db.SaveChangesAsync();

            RuntimeFunctions.Request($"Changed password of user {userId}");

            return user;
        }
        public async Task<bool> DeleteUser(long userId)
        {
            var user = await _db.Users.FindAsync(userId);

            if (user == null) return false;

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            RuntimeFunctions.Request($"Deleted user {userId}");

            return true;
        }
    }
}