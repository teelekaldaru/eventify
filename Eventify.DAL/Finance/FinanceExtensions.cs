using Eventify.Common.Classes.AutoMapper;
using Eventify.Common.Classes.Finance;
using Eventify.Domain;

namespace Eventify.DAL.Finance
{
	internal static class FinanceExtensions
	{
		public static PaymentMethod ToPaymentMethod(this DbPaymentMethod dbPaymentMethod)
		{
			return MapperWrapper.Mapper.Map<PaymentMethod>(dbPaymentMethod);
		}
	}
}
