using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetAPI.Players.Entities;
using DotNetAPI.Players.Services;

namespace DotNetAPI.Players.Controllers
{
    [ApiController]
    [Route("api/characters")]
    public class CharactersController : ControllerBase
    {
        private readonly CharactersService _charactersService;

        public CharactersController(CharactersService charactersService)
        {
            _charactersService = charactersService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter([FromBody] Character character)
        {
            try
            {
                var createdCharacter = await _charactersService.AddCharacter(character);
                return CreatedAtAction(nameof(GetCharacter), new { characterId = createdCharacter.CharacterId }, createdCharacter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{characterId}")]
        public async Task<IActionResult> GetCharacter(string characterId)
        {
            try
            {
                var character = await _charactersService.GetCharacterById(characterId);
                if (character == null)
                {
                    return NotFound();
                }

                return Ok(character);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{characterId}")]
        public async Task<IActionResult> UpdateCharacter(string characterId, [FromBody] Character updatedCharacter)
        {
            try
            {
                await _charactersService.UpdateCharacter(characterId, updatedCharacter);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{characterId}")]
        public async Task<IActionResult> DeleteCharacter(string characterId)
        {
            try
            {
                await _charactersService.DeleteCharacter(characterId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
