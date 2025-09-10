// BackEnd/DTOs/Courier/ToggleStatusRequestDto.cs
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos.Courier
{
    public class ToggleStatusRequestDto
    {
        // 使用 [Required] 特性来确保前端必须提供这个值
        [Required(ErrorMessage = "isOnline 字段是必需的。")]
        public required bool IsOnline { get; set; }
    }
}