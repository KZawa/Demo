using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Core.Domain;
using WeatherApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Infrastructure.DTOs;

namespace WeatherApp.Infrastructure.Repositories
{
    public class WeatherMeasureRepository : IWeatherMeasureRepository
    {
        private WeatherDBContext _dbContext;

        public WeatherMeasureRepository(WeatherDBContext dbContext)
        {
           _dbContext = dbContext;
        }

        public async Task CreateAsync(WeatherMeasures weatherMeasure)
        {
            var city = _dbContext.Cities.FirstOrDefault(i => i.CityId == weatherMeasure.CityId);

            if (city == null)
                throw new Exception("City doesn't exist.");

            //  _dbContext.Attach(weatherMeasure);
            //   city.WeatherMeasures.Add(weatherMeasure);

            await _dbContext.WeatherMeasures.AddAsync(weatherMeasure);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            WeatherMeasures weatheremeasureRecord = await GetAsync(id);

            _dbContext.WeatherMeasures.Remove(weatheremeasureRecord);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<WeatherMeasures>> BrowseAsync (int city_id, DateTime maximumDate)
        {
            var weatherMeasures = _dbContext.WeatherMeasures
                                            .Where(i => i.CityId == city_id &&
                                                   DateTime.UtcNow <= i.MeasureDate && i.MeasureDate <= maximumDate);

            return await Task.FromResult(weatherMeasures);
        }

        public async Task UpdateAsync(WeatherMeasures weatherMeasure)
        {
            WeatherMeasures weatheremeasureRecord = await GetAsync(weatherMeasure.CityId, weatherMeasure.MeasureDate);
            weatheremeasureRecord.CopyValues(weatherMeasure);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<WeatherMeasures> GetAsync(int id)
           => await _dbContext.WeatherMeasures.FirstOrDefaultAsync(i => i.Id == id);

        public async Task<WeatherMeasures> GetAsync(int city_id, DateTime measurementDate)
            => await _dbContext.WeatherMeasures.FirstOrDefaultAsync(i => i.CityId == city_id && i.MeasureDate.Equals(measurementDate));
    }
}
