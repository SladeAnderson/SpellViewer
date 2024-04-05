using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SpellViewer.Models.Entities;

namespace SpellViewer.Models
{
    public class CharacterActionReq
    {
        
        public class CharacterCreateReq
        {
            [Required]
            public int Userid {get; set;}
            [Required]
            public string Name {get; set;} = string.Empty;
            [Required]
            public string Race {get; set;} = string.Empty;
            [Required]
            public string Char_Class {get; set;} = string.Empty;
        }

        public class CharacterGetReq
        {
            [Required]
            public int Id {get; set;}
            [Required]
            public int UserId {get; set;}
            [Required]
            public string Name {get; set;} = string.Empty;
         }

        public class CharacterUpdateReq
        {
            [Required]
            public int Id {get; set;}
            [Required]
            public int UserId {get; set;}
            [Required]
            public string Name {get; set;} = string.Empty;
            [Required]
            public string Race {get; set;} = string.Empty;
            [Required]
            public string Char_Class {get; set;} = string.Empty;
        }

        public class CharacterUpdateSpellsReq
        {
            [Required]
            public int Id {get; set;}
            [Required]
            public int UserId {get; set;}
            [Required]
            public string Name {get; set;} = string.Empty;
            [Required]
            public List<Spell> Knownspells {get; set;} = new();
        }

        public class CharacterDeleteReq
        {
            [Required]
            public int Id {get; set;}
            [Required]
            public int UserId {get; set;}
        }
    }
}