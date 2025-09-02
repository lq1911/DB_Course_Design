using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
    public class Accept_Task
    {
        [Key, Column(Order = 0)]
        public int CourierID { get; set; }
        [ForeignKey("CourierID")]
        public Courier Courier { get; set; }

        [Key, Column(Order = 1)]
        public int DeliveryTaskID { get; set; }
        [ForeignKey("DeliveryTaskID")]
        public DeliveryTask DeliveryTask { get; set; }

        [Required]
        public DateTime AcceptTime { get; set; }
    }
}
