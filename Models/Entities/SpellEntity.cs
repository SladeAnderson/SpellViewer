using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpellViewer.Models.Entities
{
    public class Spell
    {
        [Key]
        public int Id {get; set;}
        public string Casting_time {get; set;} = string.Empty;
        public List<SpellClassesEntity> Classes { get; set; } = new();
        public SpellComponentsEntity Components { get; set; } = new();
        public string Description {get; set;} = string.Empty;
        public string Duration {get; set;} = string.Empty;
        public string Level {get; set;} = string.Empty;
        public string Name {get; set;} = string.Empty;
        public string Range {get; set;} = string.Empty;
        public bool Is_ritual {get; set;}
        public string School { get; set;} = string.Empty;
        public string Type {get; set;} = string.Empty;
    }

    public class SpellComponentsEntity
    {
        [Key]
        public int Id {get; set;}
        public bool Is_material {get; set;}
        public List<SpellMaterialsEntity>? Materials_needed {get; set;}
        public bool Is_somatic {get; set;}
        public bool Is_verbal {get; set;}
    }

    public class SpellClassesEntity
    {
        [Key]
        public int Id { get; set;}
        public string Name { get; set; } = string.Empty;
    }

    public class SpellMaterialsEntity
    {
        [Key]
        public int Id {get; set;}

        public string Materials_needed { get; set;} = string.Empty;
    }
}