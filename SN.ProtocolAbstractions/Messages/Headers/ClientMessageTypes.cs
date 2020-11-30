namespace SN.ProtocolAbstractions.Messages.Headers
{
    public enum ClientMessageTypes : short
    {
        #region Authentification (0 - 10)
        PlayerLoginSuccess = 0,
        PlayerLoginError = 1,
        SendCharacters = 2,
        CharacterSelected = 3,
        #endregion

        #region Entities (11 - ?)
        EntityPosition = 11,
        CharacterDisconnected = 12,
        #endregion
    }
}
