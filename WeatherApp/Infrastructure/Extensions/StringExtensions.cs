using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string GetHashString(this string input)
        {
            HashAlgorithm algorithm = SHA256.Create();  //or use SHA256.Create();
            
            byte [] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
