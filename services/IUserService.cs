using resevation_be.DTOs;
using resevation_be.models;

namespace resevation_be.Services
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();
        public Task<bool> AddUserAsync(UserDto user);
        public Task<UserDto?> GetUserAsync(string authorization);
    }
}