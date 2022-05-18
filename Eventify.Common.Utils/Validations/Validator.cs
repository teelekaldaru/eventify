using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventify.Common.Utils.Validations
{
	public abstract class Validator<TEntity, TParams> where TParams : IValidationParameters
	{
		public virtual async Task<IEnumerable<ValidationResult<TEntity>>> ValidateMany(
			IEnumerable<TEntity> entities,
			TParams parameters)
		{
			var results = new List<ValidationResult<TEntity>>();
			foreach (var entity1 in entities)
			{
				var entity = entity1;
				var validationMessages = await this.Validate(entity, parameters);
				if (!validationMessages.IsValid)
					results.Add(new ValidationResult<TEntity>()
					{
						InvalidEntity = entity,
						Messages = validationMessages
					});
			}
			IEnumerable<ValidationResult<TEntity>> validationResults = results;
			return validationResults;
		}

		public abstract Task<ValidationMessages> Validate(
			TEntity entity,
			TParams parameters);
	}

	public interface IValidationParameters
	{
	}
}
