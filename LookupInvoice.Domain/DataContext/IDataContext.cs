using LookupInvoice.Domain.Infrastructure.Abstract;
using System;

namespace LookupInvoice.Domain.DataContext
{
	public interface IDataContext : IDisposable
	{
		int SaveChanges();

		void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;

		void SyncObjectsStatePostCommit();
	}
}