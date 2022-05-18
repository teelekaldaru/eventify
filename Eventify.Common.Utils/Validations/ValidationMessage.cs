using Eventify.Common.Utils.Messages;
using Eventify.Common.Utils.Messages.Enums;

namespace Eventify.Common.Utils.Validations
{
    public class ValidationMessage
    {
        public string Header => Message.Header;

        public string Description => Message.Description;

        public SimpleMessage Message { get; }

        public ValidationMessageSeverity Severity
        {
            get
            {
				return Message.Type switch
				{
					MessageType.Simple or MessageType.Warning => ValidationMessageSeverity.Warning,
					_ => ValidationMessageSeverity.Error,
				};
			}
        }

        private ValidationMessage()
        {
        }

        public ValidationMessage(IMessage webMessage, ValidationMessageSeverity severity)
            : this(webMessage.Header, webMessage.Description, severity)
        {
        }

        public ValidationMessage(string message, string description, ValidationMessageSeverity severity)
        {
            Message = SimpleMessage.Validation(message, description, severity);
        }

        public ValidationMessage(ValidationCheck check)
            : this(check.Message.Header, check.Message.Description, check.Message.Severity)
        {
        }
    }
}
