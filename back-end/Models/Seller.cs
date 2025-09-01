using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models{
    public class Seller
    {
        [Key]
        public int UserID { get; set; }
        [NotMapped]
        public User User { get; set; }

        [Required]
        public DateTime SellerRegistrationTime { get; set; }

        public int ReputationPoints { get; set; } = 0;

        [Required]
        [StringLength(10)]
        public string BanStatus { get; set; } = "Normal";

        // 一对多导航属性 - 一个商家可以有多个店铺
        public ICollection<Store>? Stores { get; set; }

        // 暂时注释掉，因为数据库中没有这个字段
        // public int? AfterSaleApplicationID { get; set; }
        // [ForeignKey("AfterSaleApplicationID")]
        // public AfterSaleApplication AfterSaleApplication { get; set; }
    }
}
