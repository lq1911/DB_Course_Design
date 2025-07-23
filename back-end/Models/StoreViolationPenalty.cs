using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
    public class StoreViolationPenalty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PenaltyID { get; set; }

        [Required]
        [StringLength(255)]
        public string PenaltyReason { get; set; }

        [Required]
        public DateTime PenaltyTime { get; set; }

        [StringLength(50)]
        public string? SellerPenalty { get; set; }

        [StringLength(50)]
        public string? StorePenalty { get; set; }

        [Required]
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store Store { get; set; }
    }
}
