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

			messages.AddIfInvalid(ValidationCheck.Create(entity.AttendeeType is > AttendeeType.Unknown, ErrorMessages.AttendeeTypeInvalid, null, ValidationMessageSeverity.Error));
			if (!messages.IsValid)
			{
				return Task.FromResult(messages);
			}

			if (entity.AttendeeType == AttendeeType.Person)
			{
				messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.Name), ErrorMessages.AttendeeFirstNameRequired, null, ValidationMessageSeverity.Error));
				messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.LastName), ErrorMessages.AttendeeLastNameRequired, null, ValidationMessageSeverity.Error));
				messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.Code), ErrorMessages.AttendeePersonalCodeRequired, null, ValidationMessageSeverity.Error));

                if (!string.IsNullOrWhiteSpace(entity.Code))
                {
                    var personalCode = new PersonalCode(entity.Code);
                    messages.AddIfInvalid(ValidationCheck.Create(personalCode.IsValid(), ErrorMessages.AttendeePersonalCodeInvalid, null, ValidationMessageSeverity.Error));
                }

                if (!string.IsNullOrWhiteSpace(entity.Notes))
                {
                    messages.AddIfInvalid(ValidationCheck.Create(entity.Notes.Length <= 1500, ErrorMessages.AttendeePersonNotesMaxLengthExceeded, null, ValidationMessageSeverity.Error));
                }
			}

			if (entity.AttendeeType == AttendeeType.Company)
			{
				messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.Name), ErrorMessages.AttendeeNameRequired, null, ValidationMessageSeverity.Error));
				messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.Code), ErrorMessages.AttendeeRegisterCodeRequired, null, ValidationMessageSeverity.Error));
                messages.AddIfInvalid(ValidationCheck.Create(entity.Participants.HasValue, ErrorMessages.AttendeeParticipantsRequired, null, ValidationMessageSeverity.Error));

                if (entity.Participants.HasValue)
                {
                    messages.AddIfInvalid(ValidationCheck.Create(entity.Participants > 0, ErrorMessages.AttendeeParticipantsInvalid, null, ValidationMessageSeverity.Error));
                }

                if (!string.IsNullOrWhiteSpace(entity.Notes))
                {
                    messages.AddIfInvalid(ValidationCheck.Create(entity.Notes.Length <= 5000, ErrorMessages.AttendeeCompanyNotesMaxLengthExceeded, null, ValidationMessageSeverity.Error));
                }
			}

			messages.AddIfInvalid(ValidationCheck.Create(!string.IsNullOrWhiteSpace(entity.PaymentMethod), ErrorMessages.AttendeePaymentMethodRequired, null, ValidationMessageSeverity.Error));

			return Task.FromResult(messages);
		}

		public async Task<ValidationMessages> Validate(AttendeeSaveModel entity)
		{
			return await Validate(entity, null);
		}
	}
}
