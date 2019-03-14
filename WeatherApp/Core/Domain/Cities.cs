using System;
using System.Collections.Generic;

namespace WeatherApp.Core.Domain
{
    public partial class Cities
    {
        public Cities()
        {
            WeatherMeasures = new HashSet<WeatherMeasures>();
        }

        public Cities(string cityName)
        {
            CityName = cityName;
        }

        public int CityId { get; set; }
        public string CityName { get; set; }

        public virtual ICollection<WeatherMeasures> WeatherMeasures { get; set; }
    }
}
