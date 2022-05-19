using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eventify.Common.Classes.Finance;
using Eventify.Common.Utils.Logger;
using Eventify.Common.Utils.Messages.RequestResult;
using Eventify.Core.Base.Services;
using Eventify.DAL.Finance;

namespace Eventify.Core.Finance
{
	public interface IFinanceWebService
	{
		Task<RequestResult<IEnumerable<PaymentMethod>>> GetPaymentMethods();
	}

	internal class FinanceWebService : BaseWebService, IFinanceWebService
	{
		private readonly IFinanceRepository _financeRepository;

		public FinanceWebService(
			ILogger logger,
			IFinanceRepository financeRepository) : base(logger)
		{
			_financeRepository = financeRepository;
		}

		public async Task<RequestResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
		{
			try
			{
				var paymentMethods = await _financeRepository.GetPaymentMethods();
				return RequestResult<IEnumerable<PaymentMethod>>.CreateSuccess(paymentMethods);
			}
			catch (Exception e)
			{
				return HandleException<IEnumerable<PaymentMethod>>(e);
			}
		}
	}
}
