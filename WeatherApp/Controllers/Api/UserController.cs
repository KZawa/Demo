using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeatherApp.Infrastructure.HttpCommand.Users;
using WeatherApp.Infrastructure.Services;
using WeatherApp.Infrastructure.Settings;

namespace WeatherApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command)
        {
            try
            {
                var x = await _userService.LoginAsync(command.login, command.password);
                return Content(x);
            }
            catch(Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }
    }
}