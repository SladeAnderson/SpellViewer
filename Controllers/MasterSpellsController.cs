using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SpellViewer.Models.DTOS;
using SpellViewer.Tools;
using SpellViewer.Data;
using SpellViewer.Models.Entities;
using static SpellViewer.Models.SpellActionReq;
using SpellViewer.Repositories;
using SpellViewer.Models;

namespace SpellViewer.Controllers
{
    [ApiController]
        [Route("api/[controller]/[action]")]
    public class MasterSpellsController : Controller
    {
        private readonly SpellViewerContext dbContext;
        private readonly IMasterSpellsRepo masterSpellsRepo;

        public MasterSpellsController(SpellViewerContext dbContext, IMasterSpellsRepo masterSpellsRepo)
        {
            this.dbContext = dbContext;
            this.masterSpellsRepo = masterSpellsRepo;
        }

        // create

        [HttpPost]
        [Authorize(Policy = "GuestPolicy")]
        public async Task<IActionResult> Create([FromBody] SpellCreateReq spell)
        {
            var spellDb = dbContext.Masters_Spells;

            var newClasses = new List<SpellClassesEntity>();
            var newMaterialsNeed = new List<SpellMaterialsEntity>();
            var newComponents = new SpellComponentsEntity(); 

            if (spell.Classes.Count() > 0)
            {
                foreach (var item in spell.Classes)
                {
                    var newClass = new SpellClassesEntity(){
                        Name = item,  
                    };
                    newClasses?.Add(newClass);
                }
            }

            if (spell.Components.Materials_needed != null)
            {
                foreach (var item in spell.Components.Materials_needed)
                {
                    var newMaterials = new SpellMaterialsEntity(){
                        Materials_needed=item
                    };

                    newMaterialsNeed.Add(newMaterials);
                }
               newComponents = new SpellComponentsEntity(){
                    Is_material=spell.Components.Is_material,
                    Materials_needed=newMaterialsNeed,
                    Is_somatic=spell.Components.Is_somatic,
                    Is_verbal=spell.Components.Is_verbal,
                }; 

            } else
            {
               newComponents = new SpellComponentsEntity(){
                    Is_material=spell.Components.Is_material,
                    Is_somatic=spell.Components.Is_somatic,
                    Is_verbal=spell.Components.Is_verbal,
                };
                
            }

            var newSpell = new Spell(){
                Casting_time=spell.Casting_time,
                Classes=newClasses!,
                Components=newComponents,
                Description=spell.Description,
                Duration=spell.Duration,
                Level=spell.Level,
                Name=spell.Name,
                Range=spell.Range,
                Is_ritual=spell.Is_ritual,
                School=spell.School,
                Type=spell.Type
            };

            spellDb.Add(newSpell);

            await dbContext.SaveChangesAsync();

            var SpellToSend = newSpell;

            Console.WriteLine($"OK! sending: {SpellToSend} ");

            return Ok(SpellToSend.ToDto());
        }
        
        // Read

        [HttpPost]
        [Authorize(Policy = "GuestPolicy")]
        public async Task<IActionResult> Get([FromBody] SpellGetReq spell)
        {
            if(spell.Name != null)
            {
                var toSend = await masterSpellsRepo.Getspell(spell.Name);
                if (toSend != null)
                {
                    return Ok(toSend.ToDto());
                    
                } 
            }
            
            return NotFound("NO Spell was found!");
        }

        [HttpPost]
        [Authorize(Policy = "GuestPolicy")]
        public async Task<IActionResult> GetByClass([FromBody] SpellGetbyclassReq SpellClass)
        {
            if (SpellClass != null)
            {
                var toSend = await masterSpellsRepo.Getspells(SpellClass.ClassName);

                return Ok(toSend?.ToDto());
            }


            return NotFound(" NO Spells was found!");
        }

        // Update

        [HttpPut]
        [Authorize(Policy = "GuestPolicy")]
        public async Task<IActionResult> Update([FromBody] SpellUpdateReq spell)
        {
            int? spellId = spell.Id;

            if (spellId != null)
            {
                var spellToChange = await masterSpellsRepo.Getspell((int)spellId);
                

                if (spellToChange != null)
                {
                    Console.WriteLine($"OK! Updating: {spellToChange}");
                    if (spell.Casting_time != null)
                    {
                        Console.WriteLine($" ");
                        spellToChange.Casting_time = spell.Casting_time;
                    }


                    // Map The spells classes
                    if (spell.Classes != null)
                    {
                        List<SpellClassesEntity> newClasses = new();
                        foreach (var item in spell.Classes)
                        {
                            var newClass = new SpellClassesEntity(){
                                Name = item
                            };

                            newClasses.Add(newClass);
                        }

                        spellToChange.Classes = newClasses;
                    }

                    // map The soells Components
                    if (spell.Components != null)
                    {
                        if (spell.Components.Is_material == true)
                        {
                            List<SpellMaterialsEntity> NewmateralsList = new();
                            foreach (var item in spell.Components.Materials_needed!)
                            {
                                var newMaterial = new SpellMaterialsEntity() {
                                    Materials_needed = item
                                };

                                NewmateralsList.Add(newMaterial);
                            }

                            spellToChange.Components.Is_material = spell.Components.Is_material;
                            spellToChange.Components.Materials_needed = NewmateralsList;
                            spellToChange.Components.Is_somatic = spell.Components.Is_somatic;
                            spellToChange.Components.Is_verbal = spell.Components.Is_verbal;
                        } else
                        {
                            spellToChange.Components.Is_material = spell.Components.Is_material;
                            spellToChange.Components.Is_somatic = spell.Components.Is_somatic;
                            spellToChange.Components.Is_verbal = spell.Components.Is_verbal;
                        }

                    }

                    if (spell.Description != null)
                    {
                        spellToChange.Description = spell.Description;
                    }

                    if (spell.Duration != null)
                    {
                        spellToChange.Duration = spell.Duration;
                    }

                    if (spell.Level != null)
                    {
                        spellToChange.Level = spell.Level;
                    }

                    if (spell.Name != null)
                    {
                        spellToChange.Name = spell.Name;
                    }

                    if (spell.Range != null)
                    {
                        spellToChange.Range = spell.Range;
                    }

                    spellToChange.Is_ritual = spell.Is_ritual;

                    if (spell.School != null)
                    {
                        spellToChange.School = spell.School;
                    }

                    await dbContext.SaveChangesAsync();

                    return Ok(spellToChange.ToDto());
                }
            }

            return NotFound("Could not find spell to update!");
        }

        // Delete

        [HttpDelete]
        [Authorize(Policy = "GuestPolicy")]
        public async Task<IActionResult> Delete([FromBody] SpellDeleteReq spell)
        {
            var spellDb = dbContext.Masters_Spells;
            var spellToDelete = await masterSpellsRepo.Getspell(spell.Id);

            if (spellToDelete != null)
            {
                spellDb.Remove(spellToDelete);
                await dbContext.SaveChangesAsync();
                
            } else
            {
                return NotFound("could not find spell");
            }

            return Ok();
        }
    }
}