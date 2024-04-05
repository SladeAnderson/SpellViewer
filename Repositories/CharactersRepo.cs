    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using SpellViewer.Data;
    using SpellViewer.Models;
    using SpellViewer.Models.Entities;

namespace SpellViewer.Repositories
{
    public class CharactersRepo : ICharactersRepo
    {
        private readonly SpellViewerContext dbContext;
        private readonly IUserRepo userRepo;

        public CharactersRepo(SpellViewerContext dbContext, IUserRepo userRepo)
        {
            this.dbContext = dbContext;
            this.userRepo = userRepo;
        }

        public async Task<CharacterEntity?> GetCharacter(string Name,int userId)
        {
            //get user
            var foundUser = await userRepo.GetUserId(userId);

            if (foundUser != null)
            {
                var toSend = foundUser.Characters.FirstOrDefault(r => r.Name == Name);

                if (toSend != null)
                {
                    return toSend;
                }
                Console.WriteLine("Could not find character");
                return null;
            }
            Console.WriteLine("Could not Find User to get Character");
            return null;
        }
        
        public async Task<CharacterEntity?> GetCharacter(int id, int userId)
        {
            var foundUser = await userRepo.GetUserId(userId);

            if (foundUser != null)
            {
                var toSend = foundUser.Characters.FirstOrDefault(u => u.Id == id);

                if (toSend != null)
                {
                    return toSend;
                }
                Console.WriteLine("Could not find character");
                return null;
            }
            Console.WriteLine("Could not Find User to get Character");
            return null;
        }

        public async Task<List<CharacterEntity>?> GetAllCharacters(int userId)
        {
            var foundUser = await userRepo.GetUserId(userId);

            if (foundUser != null)
            {
                var toSend = foundUser.Characters;


                return toSend;
            }

            Console.WriteLine("Could not Find User to get all its Characters");
            return null;
        }


    }
}