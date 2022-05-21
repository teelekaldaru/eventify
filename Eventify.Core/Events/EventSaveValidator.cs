using System;
using System.Threading.Tasks;
using Eventify.Common.Utils.Messages.Enums;
using Eventify.Common.Utils.Validations;

namespace Eventify.Core.Events
{
	public interface IEventSaveValidator
	{
		Task<ValidationMessages> Validate(EventSaveModel entity);
	}

	internal class EventSaveValidator : Validator<EventSaveModel, EmptyValidationParameters>, IEventSaveValidator
	{
		public override Task<ValidationMessages> Validate(EventSaveModel entity, EmptyValidationParameters parameters)
		{
			var messages = new ValidationMessages();

			messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.Name), "Nimi on kohustuslik", null, ValidationMessageSeverity.Error));
			messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.Address), "Koht on kohustuslik", null, ValidationMessageSeverity.Error));
			messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.StartDate), "Toimumisaeg on kohustuslik", null, ValidationMessageSeverity.Error));

			if (!string.IsNullOrWhiteSpace(entity.StartDate))
			{
				messages.AddIfInvalid(ValidationCheck.Create(DateTime.TryParse(entity.StartDate, out _), "Toimumisaeg ei ole sisestatud korrektses formaadis", null, ValidationMessageSeverity.Error));
			}

			return Task.FromResult(messages);
		}

		public async Task<ValidationMessages> Validate(EventSaveModel entity)
		{
			return await Validate(entity, null);
		}
	}
}
