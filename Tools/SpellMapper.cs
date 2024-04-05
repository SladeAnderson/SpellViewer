using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpellViewer.Models.DTOS;
using SpellViewer.Models.Entities;

namespace SpellViewer.Tools
{
    public static class SpellMapper
    {
        public static Spell ToEntity(this SpellDto spell) 
        {
            return new Spell
            {
                Casting_time=spell.Casting_time,
                Classes=spell.Classes.Select(r => r.ToEntity()).ToList(),
                Components=spell.Components.ToEntity(),
                Description=spell.Description,
                Duration=spell.Duration,
                Level=spell.Level,
                Name = spell.Name,
                Range=spell.Range,
                Is_ritual=spell.Is_ritual,
                School=spell.School,
                Type=spell.Type
            };
        }

        public static List<Spell> ToEntity(this List<SpellDto> spells){
            var toSend = new List<Spell>();
            toSend.AddRange(spells.ToEntity());

            return toSend;
        }

        public static SpellClassesEntity ToEntity(this SpellClassesDto clas)
        {
            return new SpellClassesEntity
            {
                Name=clas.Name
            };
        }
        public static SpellComponentsEntity ToEntity(this SpellComponentsDto component)
        {
            
            return new SpellComponentsEntity
            {
                Is_material=component.Is_material,
                Materials_needed= component.Materials_needed?.Select(y => y.ToEntity()).ToList(),
                Is_somatic=component.Is_somatic,
                Is_verbal=component.Is_verbal
            };
        }
        public static SpellMaterialsEntity ToEntity(this SpellMaterialsDto material)
        {
            return new SpellMaterialsEntity
            {
                Materials_needed=material.Materials_needed
            };
        }

        public static SpellDto ToDto(this Spell spell)
        {
            return new SpellDto
            {
                Casting_time=spell.Casting_time,
                Classes=spell.Classes.Select(h => h.ToDto()).ToList(),
                Components=spell.Components.ToDto(),
                Description=spell.Description,
                Duration=spell.Duration,
                Level=spell.Level,
                Name = spell.Name,
                Range=spell.Range,
                Is_ritual=spell.Is_ritual,
                School=spell.School,
                Type=spell.Type
            };
        }

        public static List<SpellDto> ToDto(this List<Spell> spells){
            var toSend = new List<SpellDto>();
            toSend.AddRange(spells.ToDto());

            return toSend;
        }
        public static SpellClassesDto ToDto(this SpellClassesEntity clas)
        {
            return new SpellClassesDto
            {
                Name=clas.Name
            };
        }
        public static SpellComponentsDto ToDto(this SpellComponentsEntity component)
        {
            
            return new SpellComponentsDto
            {
                Is_material=component.Is_material,
                Materials_needed= component.Materials_needed?.Select(c => c.ToDto()).ToList(),
                Is_somatic=component.Is_somatic,
                Is_verbal=component.Is_verbal
            };
        }
        public static SpellMaterialsDto ToDto(this SpellMaterialsEntity material)
        {
            return new SpellMaterialsDto
            {
                Materials_needed=material.Materials_needed
            };
        }
    }
}