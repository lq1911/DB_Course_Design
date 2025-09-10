// BackEnd/Models/Enums/DeliveryStatus.cs
namespace BackEnd.Models.Enums
{
    public enum DeliveryStatus
    {
        To_Be_Taken,// 待取件
        Pending,    // 待取单
        Delivering, // 配送中
        Completed,  // 已完成
        Cancelled   // 已取消
    }
}