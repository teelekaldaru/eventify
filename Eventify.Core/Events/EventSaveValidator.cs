using System.Threading.Tasks;
using Eventify.Common.Utils.Validations;

namespace Eventify.Core.Events
{
	internal interface IEventSaveValidator
	{
		Task<ValidationMessages> Validate(EventSaveModel entity);
	}

	internal class EventSaveValidator : Validator<EventSaveModel, EmptyValidationParameters>, IEventSaveValidator
	{
		public override Task<ValidationMessages> Validate(EventSaveModel entity, EmptyValidationParameters parameters)
		{
			var messages = new ValidationMessages();

			return Task.FromResult(messages);
		}

		public async Task<ValidationMessages> Validate(EventSaveModel entity)
		{
			return await Validate(entity, null);
		}
	}
}
