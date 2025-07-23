using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
    public class DeliveryComplaint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComplaintID { get; set; }

        [Required]
        [StringLength(255)]
        public string ComplaintReason { get; set; }

        [Required]
        public DateTime ComplaintTime { get; set; }

        [Required]
        public int CourierID { get; set; }
        [ForeignKey("CourierID")]
        public Courier Courier { get; set; }

        [Required]
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        [Required]
        public int DeliveryTaskID { get; set; }
        [ForeignKey("DeliveryTaskID")]
        public DeliveryTask DeliveryTask { get; set; }
    }

}
