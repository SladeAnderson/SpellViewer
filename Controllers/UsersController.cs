using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpellViewer.Models; 
using SpellViewer.Models.CustomActionFilters;
using SpellViewer.Models.Entities;
using SpellViewer.Repositories;
using SpellViewer.Tools;
using SpellViewer.Data;

namespace SpellViewer.Controllers
{
    [ApiController]
        [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly SpellViewerContext dbContext;
        private readonly IUserMapper UserMapper;
        private readonly ITokenRepo TokenRepository;
        private readonly IUserRepo userRepository;
        public UsersController(SpellViewerContext dbContext, IUserMapper UserMapper, ITokenRepo tokenRepository, IUserRepo userRepository)
        {
            this.dbContext = dbContext;
            this.UserMapper = UserMapper;
            this.TokenRepository = tokenRepository;
            this.userRepository = userRepository;
        } 

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterReq user) 
        {
            var userDb = dbContext.Users;
            var userDomain = await userDb.FirstOrDefaultAsync(u=>u.Username==user.Username);
            if(userDomain!=null)
            {
                Console.WriteLine("\n Bad Request \n");
                return BadRequest("User already Exisits");
            }

            userRepository.CreatePasswordHash(user.Password, out byte[] passHash, out byte[] passSalt); 

            // ---------- okay roles -------------
            var guestRole = new RoleEntity(){
                Role="Guest"
            };
            var userRole = new RoleEntity(){
                Role="User"
            };
            var adminRole = new RoleEntity(){
                Role="Admin"
            };
            var roles = new List<RoleEntity>(){
                guestRole
            };
            if (user.Username == "Admin"){
                roles.Add(userRole);
                roles.Add(adminRole);
            }
            //-------------------------------------
            var newUser = new User(){
                Username=user.Username,
                PasswordHash=passHash,
                PasswordSalt=passSalt,
                Roles=roles,
                MoreData=user.MoreData
            };
            userDb.Add(newUser);

            Console.WriteLine("\n Ok! \n");
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    
        [HttpPost]
        [Valid]
        public async Task<IActionResult> Login([FromBody] UserLoginReq user)
        {
            Console.WriteLine($"\nLogin Request: {user.Stringify()} \n");
            var userEntity = await userRepository.GetUser(user.Username);
            if(userEntity != null) 
            {
                // check if password is correct
                if(userRepository.VerifyPasswordHash(user.Password, userEntity.PasswordHash, userEntity.PasswordSalt)){
                    Console.WriteLine("\nPassword Verified!\n");
                    var backup = new List<string>(){"guest"};
                    var totalUserEntity = await dbContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == userEntity.Id);
                    List<string> rollin;
                    if(totalUserEntity != null)
                    {
                        rollin = totalUserEntity.Roles.Select(r => r.Role).ToList();
                    } else
                    {
                        rollin = userEntity.Roles.Select(r => r.Role).ToList();
                    }
                    if (rollin == null || rollin.Count() < 1)
                    {
                        rollin = backup;
                        Console.WriteLine("Backup used");
                    }
                    Console.WriteLine($"\n{rollin.Stringify()}");

                    string? token = TokenRepository.CreateJWTToken(userEntity, rollin);
                    var BuildTokenJson = (string token)=>{
                        if (string.IsNullOrEmpty(token) == false)
                        {
                            return token;
                        }
                        return string.Empty;
                    };
                    if (token != null)
                    {
                        var tokenJSON = BuildTokenJson(token).SendAsJson();
                        Console.WriteLine($"\n Ok! sending: {tokenJSON} \n");
                        return Ok(tokenJSON);
                    }
                }
                Console.WriteLine("\nPassword Verification Failed! \n");
            }
            Console.WriteLine("\n Bad Request! \n ");

            return BadRequest("Incorrect Username or Passsord".SendAsJson());
        }

        [HttpPost]
        public async Task<IActionResult> Verify()
        {
            var userId = await userRepository.GetUserId();
            if (userId == null)
            {
                Console.WriteLine("- \n Failed To find UserID for Verification! \n -");
            } else
            {
                Console.WriteLine($"- \n UserID: {userId} \n -");
            }
            if (userId != null)
            {
                var userToSend = await userRepository.GetUserId((int)userId);
                Console.WriteLine($"- \n userToSend UserID: {userToSend?.Id} \n -");

                if(userToSend != null)
                {
                    return Ok(UserMapper.MapUser(userToSend).SendAsJson());
                } else
                {
                    var username = userRepository.GetUsername();
                    if (username != null)
                    {
                        var userEntity = await userRepository.GetUser(username);
                       Console.WriteLine($"- \n username: {username}, {userEntity?.Username}\n -");

                       if (userEntity != null)
                       {
                        var userDomainJson = UserMapper.MapUser(userEntity).SendAsJson();
                        Console.WriteLine($"- \n Ok sent! sending: {userDomainJson} \n -");
                        return Ok(userDomainJson);
                       }
                    }
                }
                Console.WriteLine("-\n BadRequest sent!\n-");
                return BadRequest("No user was found!".SendAsJson());
            } else
            {
                Console.WriteLine("-\n Unauthorized sent! \n-");
                return Unauthorized();
            }
        }

        [HttpPost]
        [Authorize(Policy = "GuestPolicy")]
        public async Task<IActionResult> VerifyGuest(){
            return Ok();
        }
        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> VerifyUser(){
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> VerifyAdmin(){
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] UserGetReq user)
        {
            if (user.Username != null)
            {
                var send = await userRepository.GetUser(user.Username);
                if (send != null)
                {
                    return Ok(UserMapper.MapUser(send).SendAsJson());
                }
            }
            return NotFound("No user was found!".SendAsJson());
        }

        [HttpPut]
        [Authorize(Policy = "GuestPolicy")]
        public async Task<IActionResult> Update([FromBody] UserUpdateReq user)
        {
            var userId = await userRepository.GetUserId();
            User? userEntity;
            if (userId != null)
            {
                userEntity = await userRepository.GetUserId((int)userId);
                if (userEntity == null)
                {
                    return NotFound();
                }
                if (user.Username != null)
                {
                    userEntity.Username = user.Username;
                }
                //-------------
                // Updating the password.
                // Ask Cole on how it should be done                
                // Need to ReEncrypt
                // if (user.Password != null)
                // {
                //     userDomain.Password = user.Password;
                // }
                //-------------
                
                if (user.MoreData != null)
                {
                    userEntity.MoreData = user.MoreData;
                }
                dbContext.SaveChanges();

                return Ok(UserMapper.MapUser(userEntity).SendAsJson());
            } else
            {
                
                return Unauthorized();
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete([FromBody] UserDeleteReq user)
        {
            var userDb = dbContext.Users;
            var userEntity = await userRepository.GetUser(user.Username.SendAsJson());
            if (userEntity == null)
            {
                return NotFound();
            } 
            userDb.Remove(userEntity);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}