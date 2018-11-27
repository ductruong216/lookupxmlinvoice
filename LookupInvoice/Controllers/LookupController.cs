using System;
using System.Data.Common;
using System.Linq;
using LookupInvoice.Domain;
using LookupInvoice.Domain.Infrastructure.Abstract;
using LookupInvoice.Domain.Infrastructure.Implementation;
using System.Web.Mvc;
using LookupInvoice.Utility;
using System.Data.SqlClient;
using System.Configuration;

namespace LookupInvoice.Controllers
{
    public class LookupController : Controller
    {
        // GET: Lookup
        private readonly IRepository<DulieuHoadon> _repository;

        public LookupController()
        {
            _repository = new BaseRepository<DulieuHoadon>();
        }


        [HttpPost]
        public ActionResult GetInvoice(string maBaoMat)
        {
            //var connectionString = String.Format("metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;" +
            //                                     "provider=System.Data.SqlClient;" +
            //                                     "provider connection string=&quot;" +
            //                                     "data source=125.212.243.249;" +
            //                                     "initial catalog=2300243582;" +
            //                                     "persist security info=True;" +
            //                                     "user id=Hoadondientu;" +
            //                                     "password=YUIO89GTHsW;" +
            //                                     "multipleactiveresultsets=True;" +
            //                                     "application name=EntityFramework&quot;");
            //var Entities = new SqlConnection(connectionString);

            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //config.ConnectionStrings.ConnectionStrings["Entities"].ConnectionString = connectionString;
            //config.ConnectionStrings.ConnectionStrings["Entities"].ProviderName = "System.Data.EntityClient";
            //config.Save(ConfigurationSaveMode.Modified);
         
            var selectedDb = new Entities();
            selectedDb.ChangeDatabase
            (
                initialCatalog: "2300243582"
            );
            var invoice = _repository.GetAll();
            var selectInvoice = invoice.SingleOrDefault(x => x.Mabaomat == maBaoMat);
            var firstPara = "2300243582";
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
}