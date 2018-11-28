using Autofac;
using Unity;

namespace LookupInvoice.Domain.Utility
{
	public class ObjectFactory
	{
		private static IUnityContainer _container;

		public static IUnityContainer Container
		{
			get => _container;
			set => _container = value;
		}

		public static T GetInstance<T>()
		{
			return Container.Resolve<T>();
		}
	}
}