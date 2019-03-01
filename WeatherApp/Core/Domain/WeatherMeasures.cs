using System;
using System.Collections.Generic;

namespace WeatherApp.Core.Domain
{
    public partial class WeatherMeasures
    {
        public int Id { get; set; }
        public decimal? Temperature { get; set; }
        public int? Humidity { get; set; }
        public decimal? Wind { get; set; }
        public decimal? Snow { get; set; }
        public decimal? Rain { get; set; }
        public int CityId { get; set; }
        public DateTime MeasureDate { get; set; }

        public virtual Cities City { get; set; }

        public void SetTemperature(decimal? temperature)
        {
            if (temperature != null)
                Temperature = temperature;
        }
        public void SetHumidity(int? humidity)
        {
            if (humidity != null)
                Humidity = humidity;
        }
        public void SetWind(decimal? wind)
        {
            if (wind != null)
                Wind = wind;
        }
        public void SetSnow(decimal? snow)
        {
            if (snow != null)
                Snow = snow;
        }
        public void SetRain(decimal? rain)
        {
            if (rain != null)
                Rain = rain;
        }

        public void SetMeasureDate(DateTime? measureDate)
        {
            if (measureDate != null)
                MeasureDate = measureDate.Value;
        }

        public void CopyValues(WeatherMeasures weatherMeasures)
        {
            SetTemperature(weatherMeasures.Temperature);
            SetHumidity(weatherMeasures.Humidity);
            SetWind(weatherMeasures.Wind);
            SetSnow(weatherMeasures.Snow);
            SetRain(weatherMeasures.Rain);
            SetMeasureDate(weatherMeasures.MeasureDate);
        }
    }
}
