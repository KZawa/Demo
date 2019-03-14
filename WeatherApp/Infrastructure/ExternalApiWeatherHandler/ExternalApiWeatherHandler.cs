using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WeatherApp.Core.Domain;
using WeatherApp.Infrastructure.DTOs;
using WeatherApp.Infrastructure.Services;

namespace WeatherApp.Infrastructure.ExternalApiWeatherHandler
{
    public class ExternalApiWeatherHandler : IExternalApiWeatherHandler
    {
        ICityService _cityService;

        public ExternalApiWeatherHandler(ICityService cityService)
        {
            _cityService = cityService;
        }

        public async Task<List<WeatherMeasures>> ConvertDataFromExternalApiToWeatherMeasureRecord(JObject jObject, string cityName)
        {
            try
            {
                var WeatherDataFromExtnernalServiceList = jObject["list"].ToList();
                List<WeatherMeasures> weatherMeasuresList = new List<WeatherMeasures>();
                var city = await _cityService.GetAsync(cityName);
                var cityId = city.CityId;
                int i = 0;
                foreach (var item in WeatherDataFromExtnernalServiceList)
                {
                    i++;
                    var temperature = item["main"] != null && item["main"]["temp"] != null ? item["main"]["temp"].Value<decimal?>() : 0;
                    var humidity = item["main"] != null && item["main"]["humidity"] != null ? item["main"]["humidity"].Value<int?>() : 0;
                    var rain = item["rain"] != null && item["rain"]["3h"] != null ? item["rain"]["3h"].Value<decimal?>() : 0;
                    var snow = item["snow"] != null && item["snow"]["3h"] != null ? item["snow"]["3h"].Value<decimal?>() : 0;
                    var wind = item["wind"] != null && item["wind"]["speed"] != null ? item["wind"]["speed"].Value<decimal?>() : 0;

                    WeatherMeasures record = new WeatherMeasures();
                    record.SetTemperature(temperature);
                    record.SetHumidity(humidity);
                    record.SetRain(rain);
                    record.SetSnow(snow);
                    record.SetWind(wind);
                    record.SetMeasureDate(item["dt_txt"].Value<DateTime>());
                    record.CityId = cityId.Value;

                    weatherMeasuresList.Add(record);
                }
                return weatherMeasuresList;
            }
            catch (Exception)
            {
                throw new Exception("Error in 'ConvertDataFromExternalApiToWeatherMeasureRecord' function.");
            }
        }
    }
}
