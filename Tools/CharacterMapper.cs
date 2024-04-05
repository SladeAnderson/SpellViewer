using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpellViewer.Models.DTOS;
using SpellViewer.Models.Entities;

namespace SpellViewer.Tools
{
    public static class CharacterMapper
    {
        public static CharacterEntity ToEntity(this CharacterDto character){
            if (character.KnownSpells.Count() > 0)
            {
                return new CharacterEntity(){
                    Name = character.Name,
                    Race = character.Race,
                    CharacterClass = character.CharacterClass,
                    KnownSpells = character.KnownSpells.ToEntity(),
                };
            }
                return new CharacterEntity(){
                    Name = character.Name,
                    Race = character.Race,
                    CharacterClass = character.CharacterClass,
                    
                };
        }

        public static CharacterDto ToDto(this CharacterEntity character){
            if (character.KnownSpells.Count() > 0)
            {
                return new CharacterDto(){
                    Name = character.Name,
                    Race = character.Race,
                    CharacterClass = character.CharacterClass,
                    KnownSpells = character.KnownSpells.ToDto()
                };
            }

            return new CharacterDto(){
                Name = character.Name,
                Race = character.Race,
                CharacterClass = character.CharacterClass
            };
        }

        public static List<CharacterDto> ToDto(this List<CharacterEntity> characters){
            var toSend = new List<CharacterDto>();
            toSend.AddRange(characters.ToDto());

            return toSend;
        }
    }
}