using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using DotNetAPI.Players.Entities;

namespace DotNetAPI.Players.Services
{
    public class CharactersService
    {
        private readonly IMongoCollection<Character> _characters;

        public CharactersService(IMongoDatabase database)
        {
            _characters = database.GetCollection<Character>("Characters");
        }

        public async Task<Character> AddCharacter(Character character)
        {
            await _characters.InsertOneAsync(character);
            return character;
        }

        public async Task<Character> GetCharacterById(string characterId)
        {
            return await _characters.Find(c => c.CharacterId == characterId).FirstOrDefaultAsync();
        }

        public async Task UpdateCharacter(string characterId, Character updatedCharacter)
        {
            var filter = Builders<Character>.Filter.Eq(c => c.CharacterId, characterId);
            var update = Builders<Character>.Update
                .Set(c => c.Class, updatedCharacter.Class)
                .Set(c => c.MaxHealth, updatedCharacter.MaxHealth)
                .Set(c => c.CharMoney, updatedCharacter.CharMoney)
                .Set(c => c.Attack, updatedCharacter.Attack)
                .Set(c => c.Defense, updatedCharacter.Defense)
                .Set(c => c.Level, updatedCharacter.Level);

            await _characters.UpdateOneAsync(filter, update);
        }

        public async Task DeleteCharacter(string characterId)
        {
            var filter = Builders<Character>.Filter.Eq(c => c.CharacterId, characterId);
            await _characters.DeleteOneAsync(filter);
        }
    }
}
