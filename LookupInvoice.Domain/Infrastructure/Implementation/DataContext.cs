using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LookupInvoice.Domain.DataContext;
using LookupInvoice.Domain.Infrastructure.Abstract;


namespace LookupInvoice.Domain.Infrastructure.Implementation
{
	public class DataContext : DbContext, IDataContextAsync
	{
		#region Private Fields

		private readonly Guid _instanceId;

		private bool _disposed;
		#endregion Private Fields

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="nameOrConnectionString"></param>
		public DataContext(string nameOrConnectionString) : base(nameOrConnectionString)
		{
			_instanceId = Guid.NewGuid();
			Configuration.LazyLoadingEnabled = false;
			Configuration.ProxyCreationEnabled = false;
		}
#if DEBUG
		//	Database.Log = Console.Write;
#endif
		public Guid InstanceId => _instanceId;

		/// <summary>
		///     Saves all changes made in this context to the underlying database.
		/// </summary>
		/// <exception cref="System.Data.Entity.Infrastructure.DbUpdateException">
		///     An error occurred sending updates to the database.</exception>
		/// <exception cref="System.Data.Entity.Infrastructure.DbUpdateConcurrencyException">
		///     A database command did not affect the expected number of rows. This usually
		///     indicates an optimistic concurrency violation; that is, a row has been changed
		///     in the database since it was queried.</exception>
		/// <exception cref="System.Data.Entity.Validation.DbEntityValidationException">
		///     The save was aborted because validation of entity property values failed.</exception>
		/// <exception cref="System.NotSupportedException">
		///     An attempt was made to use unsupported behavior such as executing multiple
		///     asynchronous commands concurrently on the same context instance.</exception>
		/// <exception cref="System.ObjectDisposedException">
		///     The context or connection have been disposed.</exception>
		/// <exception cref="System.InvalidOperationException">
		///     Some error occurred attempting to process entities in the context either
		///     before or after sending commands to the database.</exception>
		/// <seealso cref="DbContext.SaveChanges"/>
		/// <returns>The number of objects written to the underlying database.</returns>
		public override int SaveChanges()
		{
			SyncObjectsStatePreCommit();
			var changes = base.SaveChanges();
			SyncObjectsStatePostCommit();
			return changes;
		}
		/// <summary>
		///     Asynchronously saves all changes made in this context to the underlying database.
		/// </summary>
		/// <exception cref="System.Data.Entity.Infrastructure.DbUpdateException">
		///     An error occurred sending updates to the database.</exception>
		/// <exception cref="System.Data.Entity.Infrastructure.DbUpdateConcurrencyException">
		///     A database command did not affect the expected number of rows. This usually
		///     indicates an optimistic concurrency violation; that is, a row has been changed
		///     in the database since it was queried.</exception>
		/// <exception cref="System.Data.Entity.Validation.DbEntityValidationException">
		///     The save was aborted because validation of entity property values failed.</exception>
		/// <exception cref="System.NotSupportedException">
		///     An attempt was made to use unsupported behavior such as executing multiple
		///     asynchronous commands concurrently on the same context instance.</exception>
		/// <exception cref="System.ObjectDisposedException">
		///     The context or connection have been disposed.</exception>
		/// <exception cref="System.InvalidOperationException">
		///     Some error occurred attempting to process entities in the context either
		///     before or after sending commands to the database.</exception>
		/// <seealso cref="DbContext.SaveChangesAsync"/>
		/// <returns>A task that represents the asynchronous save operation.  The 
		///     <see cref="Task.Result">Task.Result</see> contains the number of 
		///     objects written to the underlying database.</returns>
		public override async Task<int> SaveChangesAsync()
		{
			return await this.SaveChangesAsync(CancellationToken.None);
		}
		/// <summary>
		///     Asynchronously saves all changes made in this context to the underlying database.
		/// </summary>
		/// <exception cref="System.Data.Entity.Infrastructure.DbUpdateException">
		///     An error occurred sending updates to the database.</exception>
		/// <exception cref="System.Data.Entity.Infrastructure.DbUpdateConcurrencyException">
		///     A database command did not affect the expected number of rows. This usually
		///     indicates an optimistic concurrency violation; that is, a row has been changed
		///     in the database since it was queried.</exception>
		/// <exception cref="System.Data.Entity.Validation.DbEntityValidationException">
		///     The save was aborted because validation of entity property values failed.</exception>
		/// <exception cref="System.NotSupportedException">
		///     An attempt was made to use unsupported behavior such as executing multiple
		///     asynchronous commands concurrently on the same context instance.</exception>
		/// <exception cref="System.ObjectDisposedException">
		///     The context or connection have been disposed.</exception>
		/// <exception cref="System.InvalidOperationException">
		///     Some error occurred attempting to process entities in the context either
		///     before or after sending commands to the database.</exception>
		/// <seealso cref="DbContext.SaveChangesAsync"/>
		/// <returns>A task that represents the asynchronous save operation.  The 
		///     <see cref="Task.Result">Task.Result</see> contains the number of 
		///     objects written to the underlying database.</returns>
		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			SyncObjectsStatePreCommit();
			var changesAsync = await base.SaveChangesAsync(cancellationToken);
			SyncObjectsStatePostCommit();
			return changesAsync;
		}
		/// <summary>
		/// Sync ObjectState
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="entity"></param>
		public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
		{
			Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
		}
		private void SyncObjectsStatePreCommit()
		{
			foreach (var dbEntityEntry in ChangeTracker.Entries())
			{
				dbEntityEntry.State = StateHelper.ConvertState(((IObjectState)dbEntityEntry.Entity).ObjectState);
			}
		}
		/// <summary>
		/// SynObject StatPost Commit
		/// </summary>
		public void SyncObjectsStatePostCommit()
		{
			foreach (var dbEntityEntry in ChangeTracker.Entries())
			{
				dbEntityEntry.State = StateHelper.ConvertState(((IObjectState)dbEntityEntry.Entity).ObjectState);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					// free other managed objects that implement
					// IDisposable only
				}
				// release any unmanaged objects
				// set object references to null
				_disposed = true;
			}
			base.Dispose(disposing);
		}
	}
}
