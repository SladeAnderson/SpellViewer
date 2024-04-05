using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpellViewer.Models
{
     public class UserRegisterReq
    {
        [Required]
        [MinLength(3,ErrorMessage = "Username is not long enough!")]
        public string Username {get; set;} = string.Empty;

        [Required]
        [MinLength(8,ErrorMessage = "Password is not long enough!")]
        public string Password {get; set;} = string.Empty;
        public string MoreData {get; set;} = string.Empty;
    }
    public class UserLoginReq
    {
        [Required]
        [MinLength(3,ErrorMessage = "Username not long enough!")]
        public string Username {get; set;} = string.Empty;
        [Required]
        [MinLength(8,ErrorMessage = "Password not long enough!")]
        public string Password {get; set;} = string.Empty; 
    }
    public class UserGetReq
    {
        public string? Username {get; set;} = string.Empty;
    }
    public class UserUpdateReq
    {
        [Required]
        public int Id {get; set;}

        [Required]
        [MinLength(3,ErrorMessage = "Username is not long enough!")]
        public string Username {get; set;} = string.Empty;

        [Required]
        [MinLength(8,ErrorMessage = "Password is not long enough")]
        public string Password {get; set;} = string.Empty;
        public string MoreData {get; set;} = string.Empty;
    }
    public class UserDeleteReq
    {
        [Required]
        public int Id{get; set;}
        [Required]
        public string Username {get; set;} = string.Empty;
        [Required]
        public string Password {get; set;} = string.Empty;
    }
}