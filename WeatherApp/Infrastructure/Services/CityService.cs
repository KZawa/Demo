using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Core.Domain;
using WeatherApp.Core.Repositories;
using WeatherApp.Infrastructure.DTOs;

namespace WeatherApp.Infrastructure.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService (ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CityDTO cityDTO)
        {
            Cities city = _mapper.Map<Cities>(cityDTO);
            await _cityRepository.CreateAsync(city);
        }

        public async Task DeleteAsync(int id)
        {
            await _cityRepository.DeleteAsync(id);
        }

        public async Task<CityDTO> GetAsync(int id, int dayCount)
        {
            Cities city = await _cityRepository.GetAsync(id, dayCount);
            return _mapper.Map<CityDTO>(city);
        }

        public async Task<CityDTO> GetAsync(string name, int dayCount)
        {
            Cities city = await _cityRepository.GetAsync(name, dayCount);
            return _mapper.Map<CityDTO>(city);
        }
    }
}
