using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLoginApp.Controllers
{
    public class HomeController : Controller
    {
     
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginUser u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using ( UsersEntities user = new UsersEntities())
                {
                    var v = user.LoginUsers.Where(a => a.Name.Equals(u.Name) && a.Passcode.Equals(u.Passcode)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.Id.ToString();  
                        Session["LogedUserFullname"] = v.Name.ToString();
                        return RedirectToAction("AfterLogin");
                    }
                }
            }
            return View(u);
        }

        public ActionResult AfterLogin()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}
