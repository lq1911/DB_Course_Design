using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
    public class DeliveryTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskID { get; set; }

        [Required]
        public DateTime EstimatedArrivalTime { get; set; }

        [Required]
        public DateTime EstimatedDeliveryTime { get; set; }

        [Column(TypeName = "decimal(10,6)")]
        public decimal? CourierLongitude { get; set; }

        [Column(TypeName = "decimal(10,6)")]
        public decimal? CourierLatitude { get; set; }

        [Required]
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        [Required]
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store Store { get; set; }
    }
}
