﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Infrastructure.DTOs;

namespace WeatherApp.Infrastructure.Services
{
    public interface ICityService
    {
        Task<CityDTO> GetAsync(int id, int dayCount = 0);
        Task<CityDTO> GetAsync(string name, int dayCount = 0);
        Task CreateAsync(CityDTO cityDTO);
        Task DeleteAsync(int id);
    }
}
