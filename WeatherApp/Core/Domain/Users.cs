using System;
using System.Collections.Generic;

namespace WeatherApp.Core.Domain
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
