// 文件路径: back-end/Services/Interfaces/IGeoHelper.cs
namespace BackEnd.Services.Interfaces
{
    public interface IGeoHelper
    {
        double CalculateDistance(decimal lat1, decimal lon1, decimal lat2, decimal lon2);
    }
}