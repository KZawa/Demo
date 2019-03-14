using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Core.Domain;
using WeatherApp.Core.Repositories;

namespace WeatherApp.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private WeatherDBContext _dbContext;

        public CityRepository(WeatherDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Cities city)
        {
            await _dbContext.Cities.AddAsync(city);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Cities city = await _dbContext.Cities.FirstOrDefaultAsync(i => i.CityId == id);
            _dbContext.Cities.Remove(city);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Cities> GetAsync(int id, int dayCount) 
        {
            //.Where(i => i.MeasureDate >= DateTime.UtcNow && i.MeasureDate < DateTime.UtcNow.AddDays(dayCount))
            //return await _dbContext.Cities.Include(x => x.WeatherMeasures.Where(i => i != null))
            //                              .FirstOrDefaultAsync(i => i.CityId == id);

            Cities city = await _dbContext.Cities.FirstOrDefaultAsync(i => i.CityId == id);

            _dbContext.Entry(city)
                      .Collection(i => i.WeatherMeasures)
                      .Query()
                      .Where(i => i.MeasureDate >= DateTime.UtcNow.AddDays(-1) && i.MeasureDate < DateTime.UtcNow.AddDays(dayCount - 1))
                      .Load();

            return city;
        }

        public async Task<Cities> GetAsync(string name, int dayCount)
        {
            Cities city = await _dbContext.Cities.FirstOrDefaultAsync(i => i.CityName == name);

            if (city != null)
            {
                _dbContext.Entry(city)
                          .Collection(i => i.WeatherMeasures)
                          .Query()
                          .Where(i => i.MeasureDate >= DateTime.UtcNow.AddDays(-1) && i.MeasureDate < DateTime.UtcNow.AddDays(dayCount - 1))
                          .Load();
            }
            else
            {
                return null;
            }
            return city;
        }
    }
}
