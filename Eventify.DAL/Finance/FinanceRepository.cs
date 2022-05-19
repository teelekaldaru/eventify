using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventify.Common.Classes.Finance;
using Eventify.DAL.Base;
using Eventify.DAL.Infrastructure;
using Eventify.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eventify.DAL.Finance
{
	public interface IFinanceRepository
	{
		Task<IEnumerable<PaymentMethod>> GetPaymentMethods();
	}
	internal class FinanceRepository : BaseRepository<AppDbContext>, IFinanceRepository
	{
		public FinanceRepository(DbContextOptions options) : base(options) { }

		public async Task<IEnumerable<PaymentMethod>> GetPaymentMethods()
		{
			var dbPaymentMethods = await GetAllAsync<DbPaymentMethod>();
			return dbPaymentMethods.Select(x => x.ToPaymentMethod());
		}
	}
}
