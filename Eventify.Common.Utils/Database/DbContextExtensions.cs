using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Common.Utils.Database
{
	public static class DbContextExtensions
	{
		public static void UpdateGuidKeyEntities(this DbContext context)
		{
			var modifiedEntries = context.ChangeTracker.Entries().Where(x => x.Entity is IHasGuidId && (x.State == EntityState.Added || x.State == EntityState.Modified));
			foreach (var entry in modifiedEntries)
			{
				var entity = (IHasGuidId)entry.Entity;
				if (entry.State == EntityState.Added)
				{
					entity.Id = Guid.NewGuid();
				}
				else
				{
					context.Entry(entity).Property(x => x.Id).IsModified = false;
				}
			}
		}
    }
}
