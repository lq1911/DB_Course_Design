// BackEnd/Dtos/User/WorkStatusDto.cs
namespace BackEnd.Dtos.Courier
{
    public class WorkStatusDto
    {
        public bool IsOnline { get; set; }
        public int OnlineHours { get; set; }
        public int OnlineMinutes { get; set; }
        public int TodayOrders { get; set; }
        public int CompletedOrders { get; set; }
        public double PunctualityRate { get; set; }
    }
}