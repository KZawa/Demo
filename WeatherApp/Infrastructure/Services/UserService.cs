using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Core.Repositories;
using WeatherApp.Infrastructure.Extensions;

namespace WeatherApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> LoginAsync(string login, string password)
        {
            if (login == null || password == null)
                throw new Exception("Email or password is null.");

            string saltedPassword = password.GetHashString();
            var user = await _userRepository.GetAsync(login);

            if (user.Password != saltedPassword)
            {
                throw new Exception("Invalid credentials.");
            }
            string token = _tokenService.CreateToken(login, password, user.Role);

            return token;
        }
    }
}
