using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos.MerchantInfo
{
    public class UpdateMerchantProfileDto
    {
        [Required]
        [Phone]
        public string Phone { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}