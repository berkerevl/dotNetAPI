using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetAPI.Players.Entities
{
    public enum CharacterClass
    {
        Rogue,
        Warrior,
        Mage,
        Archer,
    }

    public class Character
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string CharacterId { get; set; } = Guid.NewGuid().ToString();

        [BsonElement("Class")]
        [BsonRequired]
        public CharacterClass Class { get; set; }

        [BsonElement("MaxHealth")]
        [BsonRequired]
        public int MaxHealth { get; set; }

        [BsonElement("CharMoney")]
        [BsonRequired]
        public double CharMoney { get; set; }

        [BsonElement("Attack")]
        public int Attack { get; set; }

        [BsonElement("Defense")]
        public int Defense { get; set; }

        [BsonElement("Level")]
        public int Level { get; set; }

        public Character(CharacterClass characterClass, int maxHealth, double charMoney, int attack, int defense, int level)
        {
            Class = characterClass;
            MaxHealth = maxHealth;
            CharMoney = charMoney;
            Attack = attack;
            Defense = defense;
            Level = level;
        }
    }
}
