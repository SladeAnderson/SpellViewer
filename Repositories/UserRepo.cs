using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpellViewer.Data;
using SpellViewer.Models;
using SpellViewer.Models.Entities;

namespace SpellViewer.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly SpellViewerContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserRepo(SpellViewerContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<int?> GetUserId()
        {
            int? userId = null;
            var user = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = httpContextAccessor.HttpContext?.User.Identity?.Name;

            if (user != null)
            {
                userId = int.Parse(user);
                Console.WriteLine($" \n UserID: {userId} \n ");
            } else if (username != null)
            {
                var userEntity = await GetUser(username);
                userId = userEntity?.Id;
                Console.WriteLine($" \n UsernameID: {userId}\n ");
            } else
            {
                Console.WriteLine("Could not ");
            }

            return userId;
        }
        public async Task<User?> GetUserId(int id)
        {
            var userDb = dbContext.Users;
            var usernameDomain = await userDb.FirstOrDefaultAsync(u => u.Id == id);
            if (usernameDomain == null)
            {
                return null;
            }

            return usernameDomain;
        }
        public string? GetUsername()
        {
            var username = httpContextAccessor.HttpContext!.User.Identity?.Name;
            return username != null ? username : null;
        }
        public async Task<User?> GetUser(string username)
        {
            var send = await dbContext.Users.Where(u => u.Username == username).ToListAsync();
            if (send.Count() > 0)
            {
                return send[0];
            }
            else
            {
                return null;
            }
        }
        public async Task<List<User>> GetAllUsers()
        {
            var users = await dbContext.Users.ToListAsync();
            return users;
        }
        public async Task<List<string>?> GetRoles(string username)
        {
            var user = await this.GetUser(username);
            return user?.Roles?.Select(r => r.Role).ToList();
        }
        public async Task<List<string>?> GetRoles(int id)
        {
            var user = await this.GetUserId(id);
            return user?.Roles?.Select(r => r.Role).ToList();
        }
        public void CreatePasswordHash(string password, out byte[] passHash, out byte[] passSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public bool VerifyPasswordHash(string password, byte[] passHash, byte[] passSalt)
        {
            var ComputeHash = new HMACSHA512(passSalt).ComputeHash(Encoding.UTF8.GetBytes(password));
            return ComputeHash.SequenceEqual(passHash);
        }
    }
}