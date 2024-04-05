using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpellViewer.Models.Entities;

namespace SpellViewer.Data
{
    public class SpellViewerContext : DbContext
    {
        public SpellViewerContext(DbContextOptions<SpellViewerContext> dbContextOptions): base(dbContextOptions)
        {}

        public DbSet<User> Users => Set<User>();
        public DbSet<Spell> Masters_Spells => Set<Spell>();
    }
}