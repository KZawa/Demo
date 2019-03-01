using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using WeatherApp.Core.Domain;

namespace WeatherApp.Infrastructure.DTOs
{
    public class WeatherMeasureDTO
    {
        public decimal? Temperature { get; set; }
        public int? Humidity { get; set; }
        public decimal? Wind { get; set; }
        public decimal? Snow { get; set; }
        public decimal? Rain { get; set; }
        public int? CityId { get; set; }
        public DateTime? MeasureDate { get; set; }
        public int? Id { get; set; } 
    }
}
