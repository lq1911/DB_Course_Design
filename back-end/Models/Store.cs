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
        [StringLength(100)]
        public string StoreName { get; set; }

        [Required]
        [StringLength(200)]
        public string StoreAddress { get; set; }

        // 营业开始时间
        public TimeSpan? OpenTime { get; set; }

        // 营业结束时间
        public TimeSpan? CloseTime { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal AverageRating { get; set; } = 0.00m;

        [Required]
        public int MonthlySales { get; set; }

        [StringLength(1000)]
        public string StoreFeatures { get; set; }

        [Required]
        public DateTime StoreCreationTime { get; set; }

        // 店铺状态
        [StringLength(40)]
        public string? StoreState { get; set; }

        // 店铺分类
        [StringLength(40)]
        public string? StoreCategory { get; set; }

        // 店铺图片
        [StringLength(1000)]
        public string? StoreImage { get; set; }

        [Required]
        public int SellerID { get; set; }
        [ForeignKey("SellerID")]
        public Seller Seller { get; set; }
    }

}
