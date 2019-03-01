using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Core.Domain;
using WeatherApp.Infrastructure.DTOs;

namespace WeatherApp.Core.Repositories
{
    public interface IWeatherMeasureRepository
    {
        Task<WeatherMeasures> GetAsync(int id);
        Task<WeatherMeasures> GetAsync(int city_id, DateTime measurementDate);
        Task CreateAsync(WeatherMeasures weatherMeasure);
        Task UpdateAsync(WeatherMeasures weatherMeasure);
        Task<IEnumerable<WeatherMeasures>> BrowseAsync(int city_id, DateTime maximumDate);
        Task DeleteAsync(int id);
    }
}
