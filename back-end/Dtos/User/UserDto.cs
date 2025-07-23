using System;

namespace BackEnd.Dtos.User
{
    // 用于用户信息的读取
    public class UserDto
    {
        public int Id { get; set; }              
        public string Username { get; set; }     
        public string Email { get; set; }
    }
}
