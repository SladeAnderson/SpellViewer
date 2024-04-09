using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpellViewer.Data;
using SpellViewer.Models;
using SpellViewer.Models.Entities;

namespace SpellViewer.Repositories
{
    public class MasterSpellsRepo : IMasterSpellsRepo
    {
        private readonly SpellViewerContext dbContext;

        public MasterSpellsRepo(SpellViewerContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Spell>> GetAllSpells()
        {
            var spells = await dbContext.Masters_Spells.ToListAsync();
            return spells;
        }
        public async Task<Spell?> Getspell(int id)
        {
            var spellDb = dbContext.Masters_Spells;

            var FoundSpell = await spellDb.FirstOrDefaultAsync(s => s.Id == id);
            if(FoundSpell == null)
            {
                return null;
            }
            return FoundSpell;
        }
        public async Task<Spell?> Getspell(string Name)
        {
            var spellDb = dbContext.Masters_Spells;

            var FoundSpell = await spellDb.FirstOrDefaultAsync(s => s.Name.ToLower() == Name.ToLower());
            if(FoundSpell == null)
            {
                return null;
            }
            return FoundSpell;
        }

        public async Task<List<Spell>?> Getspells(string ClassName)
        {
            var spellDb = dbContext.Masters_Spells;

            var FoundSpell = await spellDb.ToListAsync();
            var FoundSpells = FoundSpell.Where(x=> x.Classes.Where(y=> y.Name == ClassName).Count() > 0).ToList();
            if(FoundSpell == null)
            {
                return null;
            }
            return FoundSpells;
        }
    }
}