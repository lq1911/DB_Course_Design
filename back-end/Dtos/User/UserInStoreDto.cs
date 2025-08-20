using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos.UserInStore
{
    public class MenuRequestDto
    {
        [Required]
        public int UserID;

        [Required]
        public int StoreID;
    }

    public class MenuResponseDto
    {
        [Required]
        public int MenuID;
    }
}
