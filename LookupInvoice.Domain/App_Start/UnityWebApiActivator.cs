//using System.Web.Http;

//using Unity.AspNet.WebApi;

//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LookupInvoice.Domain.UnityWebApiActivator), nameof(LookupInvoice.Domain.UnityWebApiActivator.Start))]
//[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(LookupInvoice.Domain.UnityWebApiActivator), nameof(LookupInvoice.Domain.UnityWebApiActivator.Shutdown))]

//namespace LookupInvoice.Domain
//{
//    /// <summary>
//    /// Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET.
//    /// </summary>
//    public static class UnityWebApiActivator
//    {
//        /// <summary>
//        /// Integrates Unity when the application starts.
//        /// </summary>
//        public static void Start() 
//        {
//            // Use UnityHierarchicalDependencyResolver if you want to use
//            // a new child container for each IHttpController resolution.
//            // var resolver = new UnityHierarchicalDependencyResolver(UnityConfig.Container);
//            var resolver = new UnityDependencyResolver(UnityConfig.Container);

//            GlobalConfiguration.Configuration.DependencyResolver = resolver;
//        }

//        /// <summary>
//        /// Disposes the Unity container when the application is shut down.
//        /// </summary>
//        public static void Shutdown()
//        {
//            UnityConfig.Container.Dispose();
//        }
//    }
//}