using System.Threading.Tasks;
using Eventify.Common.Classes.Attendees;
using Eventify.Common.Utils.Messages.Enums;
using Eventify.Common.Utils.Validations;

namespace Eventify.Core.Attendees
{
	internal interface IAttendeeSaveValidator
	{
		Task<ValidationMessages> Validate(AttendeeSaveModel entity);
	}

	internal class AttendeeSaveValidator : Validator<AttendeeSaveModel, EmptyValidationParameters>, IAttendeeSaveValidator
	{
		public override Task<ValidationMessages> Validate(AttendeeSaveModel entity, EmptyValidationParameters parameters)
		{
			var messages = new ValidationMessages();

			messages.AddIfInvalid(ValidationCheck.Create(entity.AttendeeType is > AttendeeType.Unknown, "Osavõtja tüüp ei ole täpsustatud", null, ValidationMessageSeverity.Error));
			if (!messages.IsValid)
			{
				return Task.FromResult(messages);
			}

			if (entity.AttendeeType == AttendeeType.Person)
			{
				messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.FirstName), "Eesnimi on kohustuslik", null, ValidationMessageSeverity.Error));
				messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.LastName), "Perenimi on kohustuslik", null, ValidationMessageSeverity.Error));
				messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.PersonalCode), "Isikukood on kohustuslik", null, ValidationMessageSeverity.Error));
				// TODO: check if personal code is valid estonian personal code
			}

			if (entity.AttendeeType == AttendeeType.Company)
			{
				messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.Name), "Nimi on kohustuslik", null, ValidationMessageSeverity.Error));
				messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.RegisterCode), "Registrikood on kohustuslik", null, ValidationMessageSeverity.Error));
			}

			messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.FirstName), "Maksmisviis on kohustuslik", null, ValidationMessageSeverity.Error));

			return Task.FromResult(messages);
		}

		public async Task<ValidationMessages> Validate(AttendeeSaveModel entity)
		{
			return await Validate(entity, null);
		}
	}
}
