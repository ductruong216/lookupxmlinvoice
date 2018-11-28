using System;
using LookupInvoice.Domain;
using LookupInvoice.Domain.Infrastructure.Abstract;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using LookupInvoice.Domain.Infrastructure.Implementation;
using LookupInvoice.Domain.Utility;

namespace LookupInvoice.Controllers
{
	
	public class LookupController : Controller
	{
		// GET: Lookup
		private readonly IRepository<DulieuHoadon> _repository;

		public LookupController(IRepository<DulieuHoadon> repository)
		{
			_repository = repository;
		}

		[HttpPost]
		public ActionResult GetInvoice(string maBaoMat, string maSoThue)
		{
			var dbContext = (Entities)ObjectFactory.GetInstance<IDbFactory>();
			var connection = ConfigurationManager.ConnectionStrings["Entities"];
			var ecsBuilder = new EntityConnectionStringBuilder(connection.ConnectionString);
			var sqlBuilder = new SqlConnectionStringBuilder(ecsBuilder.ProviderConnectionString) { InitialCatalog = maSoThue };
			if (dbContext.Database.Connection.State == ConnectionState.Open)
				dbContext.Database.Connection.Close();
			dbContext.Database.Connection.ConnectionString = sqlBuilder.ConnectionString;

			var invoice = _repository.GetAll();
			var selectInvoice = invoice.SingleOrDefault(x => x.Mabaomat == maBaoMat);
			var firstPara = maSoThue;
			var kyhieu = selectInvoice.Kyhieu;
			var secondPara = kyhieu.Remove(2, 1);
			var sohoadon = selectInvoice.Sohoadon.ToString();
			var url = "http://hoadondientu.link/" + firstPara + "/" + firstPara + "_" + secondPara + "_" + sohoadon + ".pdf";
			return Json(new { redirectToUrl = url }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Index()
		{
			return View();
		}
	}

	public class MultiTenantFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			
		}
	}
}