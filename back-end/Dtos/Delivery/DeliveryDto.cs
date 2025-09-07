namespace BackEnd.Dtos.Delivery
{
    public class DeliveryTaskDto
    {
        public int TaskId { get; set; }
        public string EstimatedArrivalTime { get; set; } = null!;
        public string EstimatedDeliveryTime { get; set; } = null!;
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
    }

    public class PublishTaskDto
    {
        public int SellerId { get; set; }
        public int DeliveryTaskId { get; set; }
        public string PublishTime { get; set; } = null!;
    }

    public class PublishDeliveryTaskDto
    {
        public int OrderId { get; set; }
        public string EstimatedArrivalTime { get; set; } = null!;
        public string EstimatedDeliveryTime { get; set; } = null!;
    }

    public class AcceptTaskDto
    {
        public int CourierId { get; set; }
        public int DeliveryTaskId { get; set; }
        public string AcceptTime { get; set; } = null!;
    }

    public class CourierSummaryDto
    {
        public int UserId { get; set; }
        public string? CourierRegistrationTime { get; set; }
        public string VehicleType { get; set; } = null!;
        public decimal ReputationPoints { get; set; }
        public int TotalDeliveries { get; set; }
        public int AvgDeliveryTime { get; set; }
        public decimal AverageRating { get; set; }
        public decimal MonthlySalary { get; set; }
        public string? FullName { get; set; }
        public long? PhoneNumber { get; set; }
    }

    public class OrderDeliveryInfoDto
    {
        public DeliveryTaskDto? DeliveryTask { get; set; }
        public PublishTaskDto? Publish { get; set; }
        public AcceptTaskDto? Accept { get; set; }
        public CourierSummaryDto? Courier { get; set; }
    }
}