using System.Collections.Generic;
using System.Linq;
using Eventify.Common.Utils.Messages;

namespace Eventify.Common.Utils.Validations
{
	public class ValidationResult<T>
	{
		public T InvalidEntity { get; set; }

		public IEnumerable<ValidationMessage> Messages { get; set; }

		public string GetSimpleMessage()
		{
			var messages = Messages;
			return string.Join("; ", (messages.Select(x => MessageBuilder.BuildMessage(x.Message.Header, x.Message.Description))) ?? Enumerable.Empty<string>());
		}
	}
}
