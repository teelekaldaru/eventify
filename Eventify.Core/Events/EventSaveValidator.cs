using System;
using System.Threading.Tasks;
using Eventify.Common.Classes;
using Eventify.Common.Utils.Messages.Enums;
using Eventify.Common.Utils.Validations;

namespace Eventify.Core.Events
{
	public interface IEventSaveValidator
	{
		Task<ValidationMessages> Validate(EventSaveModel entity);
	}

	public class EventSaveValidator : Validator<EventSaveModel, EmptyValidationParameters>, IEventSaveValidator
	{
		public override Task<ValidationMessages> Validate(EventSaveModel entity, EmptyValidationParameters parameters)
		{
			var messages = new ValidationMessages();

			messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.Name), ErrorMessages.EventNameRequired, null, ValidationMessageSeverity.Error));
			messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.Address), ErrorMessages.EventAddressRequired, null, ValidationMessageSeverity.Error));
			messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.StartDate), ErrorMessages.EventStartTimeRequired, null, ValidationMessageSeverity.Error));

			if (!string.IsNullOrWhiteSpace(entity.StartDate))
			{
				messages.AddIfInvalid(ValidationCheck.Create(DateTime.TryParse(entity.StartDate, out _), ErrorMessages.EventStartTimeInvalidFormat, null, ValidationMessageSeverity.Error));
			}

			return Task.FromResult(messages);
		}

		public async Task<ValidationMessages> Validate(EventSaveModel entity)
		{
			return await Validate(entity, null);
		}
	}
}
