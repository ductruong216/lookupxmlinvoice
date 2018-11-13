using LookupInvoice.Domain.Infrastructure.Abstract;

namespace LookupInvoice.Domain.Infrastructure.Implementation
{
    public class DbFactory : IDbFactory
    {
        private COM_2300376695Entities dbContext;

        public COM_2300376695Entities Init()
        {
            return dbContext ?? (dbContext = new COM_2300376695Entities());
        }

        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}