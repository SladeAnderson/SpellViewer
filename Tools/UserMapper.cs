using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpellViewer.Models.DTOS;
using SpellViewer.Models.Entities;

namespace SpellViewer.Tools
{
    public class UserMapper : IUserMapper
    {
        public UserDto MapUser(User user)
        {
            return new UserDto(){
                Id=user.Id,
                Username=user.Username,
                MoreData=user.MoreData
            };
        }

        public User MapUser(UserDto user)
        {
            return new User(){
                Id=user.Id,
                Username=user.Username,
                PasswordHash=user.PasswordHash,
                PasswordSalt=user.PasswordSalt,
                MoreData=user.MoreData
            };
        }
    }
    public interface IUserMapper
    {
        UserDto MapUser(User user);
        User MapUser(UserDto user);
    }
}