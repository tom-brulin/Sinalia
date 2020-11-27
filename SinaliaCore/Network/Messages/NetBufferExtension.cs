using Lidgren.Network;
using SinaliaCore.Models;

namespace SinaliaCore.Network.Messages
{
    public static class NetBufferExtension
    {

        public static void Write(this NetBuffer buffer, Character character)
        {
            buffer.Write(character.Id);
            buffer.Write(character.Name);
            buffer.Write((short) character.Class);
        }

        public static Character ReadCharacter(this NetBuffer buffer)
        {
            return new Character(buffer.ReadInt32(), buffer.ReadString(), (CharacterClass) buffer.ReadInt16());
        }

    }
}
