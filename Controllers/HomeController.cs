using BloodDonation.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class HomeController : Controller
    {
        BloodDonationEntities1 db = new BloodDonationEntities1();
        public ActionResult Index()
        {
            List<Workplace> list = db.Workplaces.ToList();
            ViewBag.Workplaces = list;

            return View();
        }

        public ActionResult Register(Registration model)
        {
            db.Registrations.Add(model);
            db.SaveChanges();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}