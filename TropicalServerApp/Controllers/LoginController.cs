using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TropicalServer.BLL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TropicalServerApp.Models;

namespace TropicalServerApp.Controllers
{
    public class LoginController : Controller
    {
        // static user object 
        // GET: Login
        public ActionResult Login()
        {
            TropicalUser tu = new TropicalUser();
            return View("Login", tu);
        }
        public ActionResult UserView()
        {
            return View();
        }
        public ActionResult Submit(TropicalUser tropicalUser)
        {
            tropicalUser = new TropicalUser();

            tropicalUser.LoginID = Request.Form["LoginID"];
            tropicalUser.password = Request.Form["password"];

            if (Request.Form["rememberID"] == "on")
            {
                Session["LoginID"] = tropicalUser.LoginID;
                Session["rememberID"] = "on";
            }
            else
            {
                Session["LoginID"] = "";
                Session["rememberID"] = "off";
            }

            if (ModelState.IsValid)
            {
                ReportsBLL rpBLL = new ReportsBLL();
                DataSet ds = rpBLL.Login(tropicalUser.LoginID, tropicalUser.password);

                if (ds.Tables[0].Rows.Count != 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    tropicalUser.userID = Int32.Parse(dt.Rows[0]["UserID"].ToString());
                    tropicalUser.FirstName = dt.Rows[0]["FirstName"].ToString();
                    tropicalUser.LastName = dt.Rows[0]["LastName"].ToString();
                    tropicalUser.roleID = Int32.Parse(dt.Rows[0]["roleID"].ToString());
                    tropicalUser.UserRouteNumber = Int32.Parse(dt.Rows[0]["UserRouteNumber"].ToString());
                    tropicalUser.Email = dt.Rows[0]["Email"].ToString();
                    Session["TropicalUser"] = tropicalUser;
                    return RedirectToAction("Product", "Product");
                }
                else
                {
                    ModelState.AddModelError("Invalid", "Invalid Credential"); // key and error message
                    return View("Login", tropicalUser);
                }
            }
            else
            {
                return View("Login", tropicalUser);
            }

        }
        public ActionResult ForgotUserName()
        {
            return View();
        }

        public ActionResult GetUserName()
        {
            string email = Request.Form["email"];
            ReportsBLL rpBLL = new ReportsBLL();
            DataSet ds = rpBLL.getUserName(email);
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                ViewBag.userName = dt.Rows[0]["LoginID"].ToString();
            }
            else
            {
                ViewBag.Error = "No Record";
            }
            return View("ForgotUserName");
        }

        public ActionResult GetPassword()
        {
            string email = Request.Form["email"];
            ReportsBLL rpBLL = new ReportsBLL();
            DataSet ds = rpBLL.getUserName(email);
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                ViewBag.password = dt.Rows[0]["Password"].ToString();
            }
            else
            {
                ViewBag.Error = "No Record";
            }
            return View("ForgotPassword");
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["TropicalUser"] = null;
            return RedirectToAction("Login", "Login");
        }

    }
}