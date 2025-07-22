using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
    public class Publish_Task
    {
        [Key, Column(Order = 0)]
        public int SellerID { get; set; }
        [ForeignKey("SellerID")]
        public Seller Seller { get; set; }

        [Key, Column(Order = 1)]
        public int DeliveryTaskID { get; set; }
        [ForeignKey("DeliveryTaskID")]
        public DeliveryTask DeliveryTask { get; set; }

        [Required]
        public DateTime PublishTime { get; set; }
    }
}
