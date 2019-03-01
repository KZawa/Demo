using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Core.Domain;
using WeatherApp.Core.Repositories;

namespace WeatherApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private WeatherDBContext _dbContext;

        public UserRepository(WeatherDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Users> GetAsync(string login)
        {
            //    return await Task.FromResult(db.Users);
            return (await Task.FromResult(_dbContext.Users.FirstOrDefault(x => x.Login == login)));
        }
    }
}
