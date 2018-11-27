namespace LookupInvoice.Domain.Infrastructure.Abstract
{
    public interface IDbFactory
    {
        Entities Init();
    }
}