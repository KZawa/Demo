using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Infrastructure.Services
{
    public interface IUserService
    {
        Task<string> LoginAsync(string email, string password);
    }
}
