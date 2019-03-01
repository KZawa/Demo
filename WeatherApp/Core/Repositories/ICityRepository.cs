using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Core.Domain;

namespace WeatherApp.Core.Repositories
{
    public interface ICityRepository
    {
        Task<Cities> GetAsync(int id, int dayCount);
        Task<Cities> GetAsync(string name, int dayCount);
        Task CreateAsync(Cities city);
        Task DeleteAsync(int id);
    }
}
