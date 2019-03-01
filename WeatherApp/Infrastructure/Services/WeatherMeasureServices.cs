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
    public class WeatherMeasureService : IWeatherMeasureService
    {
        private readonly IWeatherMeasureRepository _WeatherMeasureRepository;
        private readonly IMapper _mapper;

        public WeatherMeasureService(IWeatherMeasureRepository weatherMeasureRepository, IMapper mapper)
        {
            _WeatherMeasureRepository = weatherMeasureRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WeatherMeasureDTO>> BrowseAsync(int city_id, DateTime maximumDate)
        {
            var weatherMeasureRecords = await _WeatherMeasureRepository.BrowseAsync(city_id, maximumDate);
            return _mapper.Map<IEnumerable<WeatherMeasureDTO>>(weatherMeasureRecords);
        }

        public async Task CreateAsync(WeatherMeasureDTO weatherMeasureDTO)
        {
            WeatherMeasures weatherMeasure = _mapper.Map<WeatherMeasures>(weatherMeasureDTO);
            await _WeatherMeasureRepository.CreateAsync(weatherMeasure);
        }

        public async Task DeleteAsync(int id)
        {
            await _WeatherMeasureRepository.DeleteAsync(id);
        }

        public async Task<WeatherMeasureDTO> GetAsync(int id)
        {
            WeatherMeasures WeatherMeasureRecord = await _WeatherMeasureRepository.GetAsync(id);
            return _mapper.Map<WeatherMeasureDTO>(WeatherMeasureRecord);
        }

        public async Task<WeatherMeasureDTO> GetAsync(int city_id, DateTime measurementDate)
        {
            WeatherMeasures WeatherMeasureRecord = await _WeatherMeasureRepository.GetAsync(city_id, measurementDate);
            return _mapper.Map<WeatherMeasureDTO>(WeatherMeasureRecord);
        }

        public async Task UpdateAsync(WeatherMeasureDTO weatherMeasureDTO)
        {
            WeatherMeasures weatherMeasure = _mapper.Map<WeatherMeasures>(weatherMeasureDTO);
            await _WeatherMeasureRepository.UpdateAsync(weatherMeasure);
        }
    }
}
