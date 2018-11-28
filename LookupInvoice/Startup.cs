using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LookupInvoice.Startup))]

namespace LookupInvoice
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}