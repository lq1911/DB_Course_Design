using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
    public class Seller
    {
        [Key, ForeignKey("User")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public User User { get; set; }

        [Required]
        public DateTime SellerRegistrationTime { get; set; }

        public int ReputationPoints { get; set; } = 0;

        [Required]
        [StringLength(10)]
        public string BanStatus { get; set; } = "Normal";

        public int? AfterSaleApplicationID { get; set; }
        [ForeignKey("AfterSaleApplicationID")]
        public AfterSaleApplication AfterSaleApplication { get; set; }
    }
}
