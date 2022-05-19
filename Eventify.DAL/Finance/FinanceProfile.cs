using AutoMapper;
using Eventify.Common.Classes.Finance;
using Eventify.Domain;

namespace Eventify.DAL.Finance
{
	public class FinanceProfile : Profile
	{
		public FinanceProfile()
		{
			CreateMap<DbPaymentMethod, PaymentMethod>();
		}
	}
}
