using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos.Merchant
{
    // 用于接收切换营业状态的请求数据
    public class ToggleBusinessStatusRequestDto
    {
        [Required]
        public bool IsOpen { get; set; }  // 营业状态
    }
} 