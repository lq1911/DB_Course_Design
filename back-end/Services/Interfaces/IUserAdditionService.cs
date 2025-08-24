// IUserService.cs
using BackEnd.Models;
using BackEnd.DTOs.User;

namespace BackEnd.Services
{
    public interface IUserService
    {
        Task<UserProfileDto?> GetUserProfileAsync(int userId);
    }
}

