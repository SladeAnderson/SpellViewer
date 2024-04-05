using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpellViewer.Models.Entities;

namespace SpellViewer.Models
{
    public interface IMasterSpellsRepo
    {
        public Task<List<Spell>> GetAllSpells();
        public Task<Spell?> Getspell(int id);
        public Task<Spell?> Getspell(string Name);
        public Task<List<Spell>?> Getspells(string ClassName);
    }
}