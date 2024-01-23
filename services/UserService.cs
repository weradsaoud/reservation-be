using System.Data.Common;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using resevation_be.DTOs;
using resevation_be.models;

namespace resevation_be.Services
{
    public class UserService : IUserService
    {
        private AppDbContext Db { get; }

        public UserService(AppDbContext _db)
        {
            Db = _db;
        }
        public IEnumerable<User> GetUsers()
        {
            try
            {
                return Db.Users.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddUserAsync(UserDto user)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance
                    .VerifyIdTokenAsync(user.IdToken);
                string uid = decodedToken.Uid;
                User _user = new User()
                {
                    Uid = uid,
                    Email = user.Email,
                    Name = user.Name
                };
                Db.Add<User>(_user);
                Db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserDto?> GetUserAsync(string authorization)
        {
            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance
                    .VerifyIdTokenAsync(authorization);
            string uid = decodedToken.Uid;
            var user = await Db.Users.FirstOrDefaultAsync(u => u.Uid == uid);
            if (user != null)
            {
                return new UserDto()
                {
                    Name = user.Name,
                    Email = user.Email,

                };
            }
            return null;
        }
    }
}