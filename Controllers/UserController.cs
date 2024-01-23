using Microsoft.AspNetCore.Mvc;
using resevation_be.DTOs;
using resevation_be.models;
using resevation_be.Services;

namespace resevation_be.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<UserDto?> GetUser([FromHeader] string authorization)
    {
        return await _userService.GetUserAsync(authorization);
    }

    [HttpGet]
    public IEnumerable<User> GetUsers()
    {
        return _userService.GetUsers();
    }
    [HttpPost]
    public async Task<bool> AddUser([FromBody] UserDto user)
    {
        return await _userService.AddUserAsync(user);
    }
}
