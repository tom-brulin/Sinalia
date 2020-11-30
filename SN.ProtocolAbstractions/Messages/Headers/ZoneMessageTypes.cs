namespace SN.ProtocolAbstractions.Messages.Headers
{
    public enum ZoneMessageTypes : short
    {
        #region Login (0 - 10)
        PlayerLogin = 0,
        RequestCharacters = 1,
        SelectCharacter = 2,
        CharacterLoaded = 3,
        #endregion

        #region Players (11 - ?)
        PlayerDirection = 11,
        #endregion
    }
}
