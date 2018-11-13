using LookupInvoice.Domain.Infrastructure.Abstract;
using System.Web.Mvc;
using LookupInvoice.Domain;
using LookupInvoice.Domain.Infrastructure.Implementation;

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

        public string GetInvoice(string maBaoMat)
        {
            var hoadon = _repository.GetSingleById(maBaoMat);
            var firstPara = "";
            var kyhieu = hoadon.Kyhieu;
            var secondPara = kyhieu.Remove(2, 1);
            var sohoadon = hoadon.Sohoadon.ToString();
            return "http://hoadondientu.link/" + firstPara + "/" + firstPara + "_" + secondPara + "_" + sohoadon + ".pdf";
        }

        

        public ActionResult Index()
        {
            return View();
        }
    }
}