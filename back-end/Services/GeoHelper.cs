// 文件路径: back-end/Services/GeoHelper.cs
using BackEnd.Services.Interfaces; // 引用接口

namespace BackEnd.Services
{
    public class GeoHelper : IGeoHelper // 实现接口
    {
        private const double EarthRadiusKm = 6371.0;

        public double CalculateDistance(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {
            var dLat = ToRadians((double)(lat2 - lat1));
            var dLon = ToRadians((double)(lon2 - lon1));

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians((double)lat1)) * Math.Cos(ToRadians((double)lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EarthRadiusKm * c;
        }

        private static double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;  
        }
    }
}