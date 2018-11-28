using LookupInvoice.Domain.Infrastructure.Implementation;
using System.ComponentModel.DataAnnotations.Schema;

namespace LookupInvoice.Domain.Infrastructure.Abstract
{
	public interface IObjectState
	{
		[NotMapped]
		ObjectState ObjectState { get; set; }
	}
}