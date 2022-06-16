using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Data
{
    public class Geolocation
    {
        public static double Distance(City coord1, City coord2)
        {
            double latitude = coord1.Latitude;
            double longitude = coord1.Longitude;
            double latitude2 = coord2.Latitude;
            double longitude2 = coord2.Longitude;
            double d = latitude * Math.PI / 180.0;
            double d2 = latitude2 * Math.PI / 180.0;
            double num = (latitude2 - latitude) * Math.PI / 180.0;
            double num2 = (longitude2 - longitude) * Math.PI / 180.0;
            double num3 = Math.Sin(num / 2.0) * Math.Sin(num / 2.0) + Math.Cos(d) * Math.Cos(d2) * Math.Sin(num2 / 2.0) * Math.Sin(num2 / 2.0);
            double num4 = 2.0 * Math.Atan2(Math.Sqrt(num3), Math.Sqrt(1.0 - num3));
            return 6371000.0 * num4;
        }
    }
}
