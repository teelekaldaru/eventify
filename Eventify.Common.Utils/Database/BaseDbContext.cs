using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Common.Utils.Database
{
	public abstract class BaseDbContext : DbContext
	{
		protected BaseDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}

		public override int SaveChanges()
		{
			this.UpdateGuidKeyEntities();
			return base.SaveChanges();
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			this.UpdateGuidKeyEntities();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			this.UpdateGuidKeyEntities();
			return base.SaveChangesAsync(cancellationToken);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			this.UpdateGuidKeyEntities();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
	}
}
