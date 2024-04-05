using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpellViewer.Data;
using SpellViewer.Models;
using SpellViewer.Models.Entities;
using SpellViewer.Repositories;
using SpellViewer.Tools;

namespace SpellViewer.Controllers
{
    [ApiController]
        [Route("api/[controller]/[action]")]
    public class CharacterController : Controller
    {
        private readonly SpellViewerContext dbContext;
        private readonly IUserRepo UserRepository;
        private readonly ICharactersRepo CharRepo;

        public CharacterController(SpellViewerContext dbContext, IUserRepo UserRepository, ICharactersRepo CharRepo)
        {
            this.dbContext = dbContext;
            this.UserRepository = UserRepository;
            this.CharRepo = CharRepo;
        }

        [HttpPost]
        [Authorize(Policy = "GuestPolicy")]
        public async Task<IActionResult> Create([FromBody] CharacterActionReq.CharacterCreateReq req)
        {
            // get user
            var foundUser = await UserRepository.GetUserId(req.Userid);
            Console.WriteLine($"Found! User:{foundUser}");

            if (foundUser != null)
            {
                // create character
                var newCharacter = new CharacterEntity(){
                    Name = req.Name,
                    Race = req.Race,
                    CharacterClass = req.Char_Class,
                };  


                // appened character to user charlist &
                // Save changes

                foundUser.Characters.Add(newCharacter);

                await dbContext.SaveChangesAsync();

                return Ok(newCharacter.ToDto());
                
            }

            return NotFound("Could not find user for character creation");
        }

        [HttpPost]
        [Authorize(Policy = "GuestPolicy")]
        public async Task<IActionResult> Get([FromBody] CharacterActionReq.CharacterGetReq req)
        {
            
            var foundUser = await UserRepository.GetUserId(req.UserId);

            if (foundUser != null)
            {
                var toSend = await CharRepo.GetCharacter(req.Id,foundUser.Id);


                return Ok(toSend?.ToDto());
            }

            return NotFound("Could not find user!");
        }

        [HttpPost]
        [Authorize(Policy = "GuestPolicy")]
        public async Task<IActionResult> GetAll([FromBody] CharacterActionReq.CharacterGetReq req)
        {
            
            var foundUser = await UserRepository.GetUserId(req.UserId);

            if (foundUser != null)
            {
                var toSend = await CharRepo.GetAllCharacters(foundUser.Id);
                

                return Ok(toSend?.ToDto());
            }

            return NotFound("Could not find user!");
        }

        [HttpPut]
        [Authorize(Policy = "GuestPolicy")]
        public async Task<IActionResult> Update([FromBody] CharacterActionReq.CharacterUpdateReq req)
        {
            var foundUser = await UserRepository.GetUserId(req.UserId);
            if (foundUser != null)
            {
                var theCharacter = await CharRepo.GetCharacter(req.Id, foundUser.Id);
                if (theCharacter != null)
                {
                    

                    if (req.Name != null)
                    {
                        theCharacter.Name = req.Name;
                    }

                    if (req.Race != null)
                    {
                        theCharacter.Race = req.Race;
                    }

                    if (req.Char_Class != null)
                    {
                        theCharacter.CharacterClass = req.Char_Class;
                    }

                    await dbContext.SaveChangesAsync();

                    return Ok(theCharacter.ToDto());
                }
            }

            return NotFound("Could not find user!"); 
        }

        [HttpDelete]
        [Authorize(Policy = "GuestPolicy")]
        public async Task<IActionResult> Delete([FromBody] CharacterActionReq.CharacterDeleteReq req)
        {
            var foundUser = await UserRepository.GetUserId(req.UserId);
            if (foundUser != null)
            {
                var toDelete = await CharRepo.GetCharacter(req.Id,foundUser.Id);
                
                if (toDelete != null)
                {
                    
                    foundUser.Characters?.Remove(toDelete);

                    await dbContext.SaveChangesAsync();
                    
                    return Ok("Ok! Deleted!");
                }

            }

            return NotFound("Could not find user!");
        }
    }
}