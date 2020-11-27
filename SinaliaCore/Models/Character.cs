namespace SinaliaCore.Models
{
    public class Character
    {

        public int Id { get; }
        public string Name { get; }
        public CharacterClass Class { get; }

        public Character(int id, string name, CharacterClass characterClass)
        {
            Id = id;
            Name = name;
            Class = characterClass;
        }

    }
}
