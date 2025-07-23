using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
   public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreID { get; set; }

        [Required]
        [StringLength(50)]
        public string StoreName { get; set; }

        [Required]
        [StringLength(100)]
        public string StoreAddress { get; set; }

        [Required]
        public DateTime BusinessHours { get; set; } // 若需要营业开始/结束时间建议拆分

        [Column(TypeName = "decimal(10,2)")]
        public decimal AverageRating { get; set; } = 0.00m;

        [Required]
        public int MonthlySales { get; set; }

        [Required]
        [StringLength(500)]
        public string StoreFeatures { get; set; }

        [Required]
        public DateTime StoreCreationTime { get; set; }

        [Required]
        public int SellerID { get; set; }
        [ForeignKey("SellerID")]
        public Seller Seller { get; set; }
    }

}
