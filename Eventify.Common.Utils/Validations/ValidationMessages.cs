using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Eventify.Common.Utils.Messages;
using Eventify.Common.Utils.Messages.Enums;

namespace Eventify.Common.Utils.Validations
{
    public class ValidationMessages : IEnumerable<ValidationMessage>, IEnumerable
    {
        private readonly List<ValidationMessage> _messages = new();

        public bool IsValid => !_messages.Any();

        public bool IsValidOrOnlyWarnings
        {
            get
            {
                if (!IsValid)
                {
                    return _messages.All((x) => x.Severity == ValidationMessageSeverity.Warning);
                }

                return true;
            }
        }

        public ValidationMessage this[int index]
        {
            get
            {
                return _messages[index];
            }
            set
            {
                _messages.Insert(index, value);
            }
        }

        public IEnumerator<ValidationMessage> GetEnumerator()
        {
            return _messages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddIfInvalid(ValidationCheck check)
        {
            if (!check.IsValid)
            {
                _messages.Add(new ValidationMessage(check));
            }
        }

        public void Add(string header, string description, ValidationMessageSeverity severity)
        {
            _messages.Add(new ValidationMessage(header, description, severity));
        }

        public void AddRange(ValidationMessages messages)
        {
            _messages.AddRange(messages);
        }

        public void AddRange(IEnumerable<ValidationMessage> messages)
        {
            if (messages != null)
            {
                _messages.AddRange(messages);
            }
        }

        public string GetApiMessages()
        {
            return string.Join("; ", _messages.Select((x) => x.ToString()));
        }

        public string GetApiMessagesWithSeparatedWarnings(bool printType)
        {
            var text = string.Join("|", from x in _messages
                                           where x.Severity == ValidationMessageSeverity.Error
                                           select string.Format("{0}{1}", printType ? "ERROR: " : string.Empty, x.Message));
            var text2 = string.Join("|", from x in _messages
                                            where x.Severity == ValidationMessageSeverity.Warning
                                            select string.Format("{0}{1}", printType ? "WARNING: " : string.Empty, x.Message));
            if (!string.IsNullOrWhiteSpace(text))
            {
                if (!string.IsNullOrWhiteSpace(text2))
                {
                    return string.Join("|", text, text2);
                }

                return text;
            }

            return text2;
        }

        public IEnumerable<SimpleMessage> GetWebMessages()
        {
            return _messages.Select((x) => x.Message);
        }

        public ValidationMessages()
        {
        }

        public ValidationMessages(IEnumerable<ValidationMessage> messages)
        {
            _messages = messages.ToList();
        }
    }
}
