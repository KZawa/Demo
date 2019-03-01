using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Infrastructure.Services
{
    public interface ITokenService
    {
        string CreateToken(string name, string password, string role);

    }
}
