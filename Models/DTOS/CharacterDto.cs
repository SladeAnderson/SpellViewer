using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpellViewer.Models.DTOS
{
    public class CharacterDto
    {
        [Key]
        public int Id { get; set;}
        public string Name { get; set;} = string.Empty;
        public string Race {get; set;} = string.Empty;
        public string CharacterClass {get; set;} = string.Empty;
        public List<SpellDto> KnownSpells { get; set; } = new(); 
    }
}