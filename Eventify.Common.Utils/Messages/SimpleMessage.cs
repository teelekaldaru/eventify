using Eventify.Common.Utils.Messages.Enums;

namespace Eventify.Common.Utils.Messages
{
    public interface IMessage
    {
        string Header { get; }

        string Description { get; }
    }

    public class SimpleMessage : IMessage
    {
        public string Header { get; set; }

        public string Description { get; set; }

        public bool IsSimpleMessage => Type == MessageType.Simple;

        public bool IsError => Type == MessageType.Error;

        public bool IsWarning => Type == MessageType.Warning;

        public bool IsWarningOrSimpleMessage
        {
            get
            {
                if (!IsSimpleMessage)
                {
                    return IsWarning;
                }

                return true;
            }
        }

        public MessageType Type { get; set; }

        private SimpleMessage()
        {
        }

        public static SimpleMessage Simple(string header, string description)
        {
            return new SimpleMessage
            {
                Header = header,
                Description = description,
                Type = MessageType.Simple
            };
        }

        public static SimpleMessage Error(string header, string description)
        {
            return new SimpleMessage
            {
                Header = header,
                Description = description,
                Type = MessageType.Error
            };
        }

        public static SimpleMessage Validation(string header, string description, ValidationMessageSeverity severity)
        {
            return new SimpleMessage
            {
                Header = header,
                Description = description,
                Type = severity == ValidationMessageSeverity.Warning ? MessageType.Warning : MessageType.Error
            };
        }

        public static SimpleMessage CopyAndOverrideHeader(SimpleMessage source, string headerOverride)
        {
            if (string.IsNullOrWhiteSpace(headerOverride))
            {
                return source;
            }

            var header = source.Header;
            return new SimpleMessage
            {
                Type = source.Type,
                Description = header,
                Header = headerOverride
            };
        }

        public override string ToString()
        {
            return MessageBuilder.BuildMessage(Header, Description);
        }
    }
}
