using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Infrastructure.DTOs;

namespace WeatherApp.Infrastructure.Services
{
    public interface IWeatherMeasureService
    {
        Task<WeatherMeasureDTO> GetAsync(int id);
        Task<WeatherMeasureDTO> GetAsync(int city_id, DateTime measurementDate);
        Task CreateAsync(WeatherMeasureDTO weatherMeasureDTO);
        Task UpdateAsync(WeatherMeasureDTO weatherMeasureDTO);
        Task <IEnumerable<WeatherMeasureDTO>> BrowseAsync(int city_id, DateTime maximumDate);
        Task DeleteAsync(int id);
    }
}
