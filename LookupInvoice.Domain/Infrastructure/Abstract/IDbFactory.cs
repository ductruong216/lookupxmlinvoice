using System;

namespace LookupInvoice.Domain.Infrastructure.Abstract
{
    public interface IDbFactory : IDisposable
	{
		// Entities Init();
		int SaveChanges();
	}
}