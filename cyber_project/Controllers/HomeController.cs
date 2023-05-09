using cyber_project.Models;
using cyber_project.Services.Business;
using cyber_project.Services.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using cyber_project;
using System.Web.Security;

namespace cyber_project.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Register()
        {
            return View("Register");

        }

        [HttpPost]
        public ActionResult Register(RegisterModel rm)
        {
            DataService DataService = new DataService();
            Boolean success = DataService.Acc(rm);

            RsaEncryption rsa = new RsaEncryption();
            string publicKey = rsa.GetPublicKey();
            if (success)
            {
                RegisterDAO daoRegister = new RegisterDAO();
                daoRegister.RegisterData(rm);
                return View("RegistSuccessful", rm);
            }
            else
            {
                return View("RegistFailure");

            }

        }
        public ActionResult Login()
        {
            return View("Login");

        }
        [HttpPost]
        public ActionResult Login(UserModel userModel)
        {
            SecurityService securityService = new SecurityService();
            Boolean success = securityService.Authenticate(userModel);

            if (success)
            {
                FormsAuthentication.SetAuthCookie(userModel.Username, false);
                return View("LoginSuccess", userModel);
            }
            else
            {
                return View("LoginFailure");

            }
        }

    }
}