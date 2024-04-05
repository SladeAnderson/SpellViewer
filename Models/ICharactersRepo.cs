using SpellViewer.Models.Entities;

namespace SpellViewer.Repositories
{
    public interface ICharactersRepo
    {
        public Task<CharacterEntity?> GetCharacter(string Name,int userId);
        public Task<CharacterEntity?> GetCharacter(int id, int userId);
        public Task<List<CharacterEntity>?> GetAllCharacters(int userId);
    }
}