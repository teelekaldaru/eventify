using System.Threading.Tasks;
using Eventify.Core.Finance;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.Web.Controllers
{
	[Route("api/finances")]
	public class FinanceController : ControllerBase
	{
		private readonly IFinanceWebService _financeWebService;

		public FinanceController(IFinanceWebService financeWebService)
		{
			_financeWebService = financeWebService;
		}

		[HttpGet("payment-methods")]
		public async Task<JsonResult> GetPaymentMethods()
		{
			var result = await _financeWebService.GetPaymentMethods();
			return new JsonResult(result);
		}
	}
}
