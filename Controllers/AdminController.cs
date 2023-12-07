using BloodDonation.Context;
using BloodDonation.Models;
using PagedList.Mvc;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace BloodDonation.Controllers
{
    public class AdminController : Controller
    {
        BloodDonationEntities1 db = new BloodDonationEntities1();

        // GET: Default
        public ActionResult Index(int? page, int? pageSize)
        {
            var isLogin = Session[string.Format("isLogin")] as string;
            if(isLogin != "true")
            {
                return RedirectToAction("Login");
            }

            int currentPage = page ?? 1;
            int currentPageSize = pageSize ?? 10; // PageSize mặc định

            var registrations = db.Registrations.OrderBy(p => p.Id)
                                        .Skip((currentPage - 1) * currentPageSize)
                                        .Take(currentPageSize)
                                        .ToList();

            var totalProducts = db.Registrations.Count();

            var model = registrations.ToPagedList(currentPage, currentPageSize);

            return View(model);
        }

        public ActionResult Search(FilterRegistration filter)
        {
            int currentPage = filter.page ?? 1;
            int currentPageSize = filter.pageSize ?? 10; // PageSize mặc định

            var registrations = db.Registrations.Where(t => t.FullName.Contains(filter.FullName) && t.Phone.Contains(filter.Phone) && t.Email.Contains(filter.Email)).OrderBy(p => p.Id)
                                        .Skip((currentPage - 1) * currentPageSize)
                                        .Take(currentPageSize)
                                        .ToList();

            var totalProducts = db.Registrations.Count();

            var model = registrations.ToPagedList(currentPage, currentPageSize);

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult AdminLogin(string username, string password)
        {
            if(username == "admin" && password == "admin")
            {
                Session[string.Format("isLogin")] = "true";
            }

            return Json(Session[string.Format("isLogin")] as string, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session[string.Format("isLogin")] = "false";

            return RedirectToAction("Login");
        }
    }
}