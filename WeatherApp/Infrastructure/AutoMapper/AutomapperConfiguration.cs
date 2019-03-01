using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Core.Domain;
using WeatherApp.Infrastructure.DTOs;

namespace WeatherApp.Infrastructure.AutoMapper
{
    public class AutomapperConfiguration
    {
        public static IMapper Initialize()
        {
           return new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<WeatherMeasures, WeatherMeasureDTO>();
               cfg.CreateMap<WeatherMeasureDTO, WeatherMeasures>();
               cfg.CreateMap<Cities, CityDTO>();
               cfg.CreateMap<CityDTO, Cities>();
           })
            .CreateMapper();
        }
    }
}
