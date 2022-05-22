using System.Threading.Tasks;
using Eventify.Common.Classes;
using Eventify.Common.Classes.Attendees;
using Eventify.Common.Utils.Messages.Enums;
using Eventify.Common.Utils.Validations;

namespace Eventify.Core.Attendees
{
	public interface IAttendeeSaveValidator
	{
		Task<ValidationMessages> Validate(AttendeeSaveModel entity);
	}

    public class AttendeeSaveValidator : Validator<AttendeeSaveModel, EmptyValidationParameters>, IAttendeeSaveValidator
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
				messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.Name), "Eesnimi on kohustuslik", null, ValidationMessageSeverity.Error));
				messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.LastName), "Perenimi on kohustuslik", null, ValidationMessageSeverity.Error));
				messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.Code), "Isikukood on kohustuslik", null, ValidationMessageSeverity.Error));

                if (!string.IsNullOrWhiteSpace(entity.Code))
                {
                    var personalCode = new PersonalCode(entity.Code);
                    messages.AddIfInvalid(ValidationCheck.Create(personalCode.IsValid(), "Isikukood ei ole korrektne", null, ValidationMessageSeverity.Error));
                }
			}

			if (entity.AttendeeType == AttendeeType.Company)
			{
				messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.Name), "Nimi on kohustuslik", null, ValidationMessageSeverity.Error));
				messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.Code), "Registrikood on kohustuslik", null, ValidationMessageSeverity.Error));
                messages.AddIfInvalid(ValidationCheck.Create(entity.Participants.HasValue, "Osalejate arv on kohustuslik", null, ValidationMessageSeverity.Error));

                if (entity.Participants.HasValue)
                {
                    messages.AddIfInvalid(ValidationCheck.Create(entity.Participants > 0, "Osalejate arv peab olema vähemalt 1", null, ValidationMessageSeverity.Error));
                }
			}

			messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.PaymentMethod), "Maksmisviis on kohustuslik", null, ValidationMessageSeverity.Error));

			return Task.FromResult(messages);
		}

		public async Task<ValidationMessages> Validate(AttendeeSaveModel entity)
		{
			return await Validate(entity, null);
		}
	}
}
