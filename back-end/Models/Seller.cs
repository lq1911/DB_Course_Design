using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Models.Enums;

namespace BackEnd.Models
{
    public class Seller
    {
        // 商家类
        // 主码：UserID
        // 外码：UserID

        [Key, ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; } = null!;

        [Required]
        public DateTime SellerRegistrationTime { get; set; }

        public int ReputationPoints { get; set; } = 0;

        [Required]
        public SellerState BanStatus { get; set; } = SellerState.Normal;

        // 商家和店铺一对一
        public Store? Store { get; set; }
    }
}
