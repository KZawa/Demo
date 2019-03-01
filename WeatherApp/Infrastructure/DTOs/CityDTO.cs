using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Core.Domain;

namespace WeatherApp.Infrastructure.DTOs
{
    public class CityDTO
    {
        public string CityName { get; set; }
        public int? CityId { get; set; }
        public virtual ICollection<WeatherMeasureDTO> WeatherMeasures { get; set; }
    }
}
