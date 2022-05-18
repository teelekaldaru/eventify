using System.Threading.Tasks;
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

			return Task.FromResult(messages);
		}

		public async Task<ValidationMessages> Validate(AttendeeSaveModel entity)
		{
			return await Validate(entity, null);
		}
	}
}
