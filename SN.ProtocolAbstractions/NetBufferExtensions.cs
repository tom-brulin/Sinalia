using Lidgren.Network;
using SN.Global.Models;

namespace SN.ProtocolAbstractions
{
    public static class NetBufferExtensions
    {

        #region Character
        public static void Write(this NetBuffer buffer, Character character)
        {
            buffer.Write(character.AccountPersistentId);
            buffer.Write(character.Id);
            buffer.Write(character.Name);
            buffer.Write(character.X);
            buffer.Write(character.Y);
        }

        public static Character ReadCharacter(this NetBuffer buffer)
        {
            return new Character(buffer.ReadString(), buffer.ReadInt32(), buffer.ReadString(), buffer.ReadFloat(), buffer.ReadFloat());
        }
        #endregion

    }
}
