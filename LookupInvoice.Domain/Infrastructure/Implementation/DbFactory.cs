using LookupInvoice.Domain.Infrastructure.Abstract;

namespace LookupInvoice.Domain.Infrastructure.Implementation
{
    public class DbFactory : IDbFactory
    {
        private Entities dbContext;

        public Entities Init()
        {
            return dbContext ?? (dbContext = new Entities());
        }

        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}