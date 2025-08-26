// IUserService.cs
using BackEnd.Models;
using BackEnd.DTOs.User;

namespace BackEnd.Services
{
    public interface IUserAdditionService
    {
        Task<UserProfileDto?> GetUserProfileAsync(int userId);
        Task<UserAddressDto?> GetUserAddressAsync(int userId);
    }

}


