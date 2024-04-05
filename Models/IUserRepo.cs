using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpellViewer.Models.Entities;

namespace SpellViewer.Models
{
    public interface IUserRepo
    {
        public Task<int?> GetUserId();
        public Task<User?> GetUser(string username);
        public Task<User?> GetUserId(int id);
        public string? GetUsername();
        public Task<List<User>> GetAllUsers();
        public Task<List<string>?> GetRoles(string username);
        public Task<List<string>?> GetRoles(int id);
        public void CreatePasswordHash(string password, out byte[] passHash, out byte[] passSalt);
        public bool VerifyPasswordHash(string password, byte[] passHash, byte[] passSalt);
    }
}