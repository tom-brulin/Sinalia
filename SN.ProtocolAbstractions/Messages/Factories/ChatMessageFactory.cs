﻿using SN.GlobalAbstractions.Logging;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.ProtocolAbstractions.Messages.Factories
{
    public class ChatMessageFactory : IMessageFactory
    {

        private readonly ILoggingService loggingService;

        public ChatMessageFactory(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        public SNMessageData GetMessageData(short type)
        {
            ChatMessageTypes header = (ChatMessageTypes)type;
            SNMessageData message = null;

            switch (header)
            {
                default:
                    loggingService.Log($"Cannot build chat message {header.ToString()}", LogMessageType.WARNING);
                    break;
            }

            return message;
        }

    }
}
