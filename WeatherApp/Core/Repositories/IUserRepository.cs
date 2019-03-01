﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Core.Domain;

namespace WeatherApp.Core.Repositories
{
    public interface IUserRepository
    {
        Task<Users> GetAsync(string login);
    }
}
