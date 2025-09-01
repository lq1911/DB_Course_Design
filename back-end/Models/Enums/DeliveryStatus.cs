// BackEnd/Models/Enums/DeliveryStatus.cs
namespace BackEnd.Models.Enums
{
    public enum DeliveryStatus
    {
        Pending,    // 待处理 (骑手还未接受)
        Delivering, // 配送中 (骑手已接受)
        Completed,  // 已完成
        Cancelled   // 已取消
    }
}