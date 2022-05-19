using System;

namespace Eventify.Common.Utils.Database
{
	public interface IHasGuidId
	{
		Guid Id { get; set; }
	}
}
