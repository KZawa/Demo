using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Core.Domain;

namespace WeatherApp.Infrastructure.ExternalApiWeatherHandler
{
    public interface IExternalApiWeatherHandler
    {
        Task<List<WeatherMeasures>> ConvertDataFromExternalApiToWeatherMeasureRecord(JObject jObject, string cityName);
    }
}
