using LookupInvoice.Domain;
using LookupInvoice.Domain.Infrastructure.Abstract;
using LookupInvoice.Domain.Infrastructure.Implementation;
using LookupInvoice.Domain.Utility;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace LookupInvoice
{
	/// <summary>
	/// Specifies the Unity configuration for the main container.
	/// </summary>
	public static class UnityConfig
	{
		public static void RegisterComponents()
		{
			RegisterTypes(ObjectFactory.Container);
		}

		public static void RegisterTypes(IUnityContainer container)
		{
			container.RegisterType<IDbFactory, Entities>(new PerRequestLifetimeManager());
			// Register interface

			container.RegisterType<IRepository<DulieuHoadon>, BaseRepository<DulieuHoadon>>(
				new PerRequestLifetimeManager());

			// Repository
			GlobalConfiguration.Configuration.DependencyResolver = new Unity.AspNet.WebApi.UnityDependencyResolver(ObjectFactory.Container);
			DependencyResolver.SetResolver(new Unity.AspNet.Mvc.UnityDependencyResolver(ObjectFactory.Container));

			//var container = builder.Build();
			//      ObjectFactory.Container = container;
			//      DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
			//      GlobalConfiguration.Configuration.DependencyResolver =
			//       new AutofacWebApiDependencyResolver((IContainer)container);
		}
	}
}