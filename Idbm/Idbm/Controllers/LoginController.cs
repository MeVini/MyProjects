using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Configuration;
using Idbm.Models;
using System.Runtime.InteropServices;
using IdbmDAL.Managers;
using System.Text;
using log4net;
using log4net.Config;
using CaptchaMvc.HtmlHelpers;
using Recaptcha;
using Idbm.Helper;
namespace Idbm.Controllers
{
    public class LoginController : BaseController
    {
        //
        // GET: /Login/
        LoginManager login = new LoginManager();
        Logins logs = new Logins();
        Login1 logDAL = new Login1();
        Comment comment = new Comment();
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(LoginController));
        [AllowAnonymous]
        public ActionResult AdminLogin()
        {
            return View("Login");
        }

        [HttpPost]
       
        [RecaptchaControlMvc.CaptchaValidator]
        public ActionResult Register(Models.Logins user, bool captchaValid, string captchaErrorMessage)
        {
            if (ModelState.IsValid)
            {
                if (captchaValid)
                {
                    Response.Write("Captcha is correct");
                }
                ModelState.AddModelError("", captchaErrorMessage);
            }
            try
            {
                String EncryptPassword = user.Password;
                EncryptPassword = Helper.Encryption.Encrypt(EncryptPassword);
                logDAL = login.RegisterAdmin(user.FirstName, user.LastName, user.Email, EncryptPassword, user.FavouriteGenre);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            Session["LogedUserName"] = logDAL.FirstName.ToString();
            Session["LogedUserGenre"] = logDAL.FavouriteGenre.ToString();
            return RedirectToAction("MyPage", "Movie");
        }
         
        public ActionResult AlreadyRegistered()
        {
            return View();
        }

        
        public ActionResult Registered(String Email, String Password)
        {
            String EncryptPassword = Password;

            try
            {
                EncryptPassword = Helper.Encryption.Encrypt(EncryptPassword);
                logDAL = login.Registered(Email, EncryptPassword);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            if (logDAL == null)
            {
                TempData["Error"] = "Login Failed. Please enter valid Email-Id and Password";
                return RedirectToAction("AlreadyRegistered", "Login");
            }
            Session["LogedUserId"] = logDAL.Id.ToString();
            Session["LogedUserName"] = logDAL.FirstName.ToString();
            Session["LogedUserGenre"] = logDAL.FavouriteGenre.ToString();
            return RedirectToAction("MyPage", "Movie");
        }
        public ActionResult StoreComments(String title, String message, String rating)
        {
            int rate = Convert.ToInt32(rating);
            String userid = @Session["LogedUserId"].ToString();
            int useId = Convert.ToInt32(userid);
            login.SaveComments(title, message, rate, useId);
            return Redirect(HttpContext.Request.UrlReferrer.OriginalString);

        }
        public ActionResult GetCommentForMovieTitle(String title)
        {
            List<Comment> CommentList = new List<Comment>();
            CommentList= login.GetCommentForMovieTitle(title);
            return PartialView("_AllComments",CommentList);
        }
        
        public ActionResult CheckExternalLoginRegister()
        {
            String Email = Session["LogedUserEmail"].ToString();
            logDAL = login.CheckExternalEmail(Email);
            if (logDAL!=null)
            {
                Session["LogedUserId"] = logDAL.Id.ToString();
                Session["LogedUserName"] = logDAL.FirstName.ToString();
                Session["LogedUserGenre"] = logDAL.FavouriteGenre.ToString();
                return RedirectToAction("MyPage","Movie");
            }
            return RedirectToAction("CheckExternalLoginRegister","Login");
        }

        public ActionResult ExternalLoginRegister(Models.Logins user)
        {
            user.FirstName= Session["LogedUserName"].ToString();
            user.LastName=Session["LogedUserLastName"].ToString();
            user.Email=Session["LogedUserEmail"].ToString();

            String EncryptPassword = "ExternalLogin";
            EncryptPassword = Helper.Encryption.Encrypt(EncryptPassword);

            logDAL = login.RegisterAdmin(user.FirstName, user.LastName, user.Email, EncryptPassword, user.FavouriteGenre);
            Session["LogedUserName"] = logDAL.FirstName.ToString();
            Session["LogedUserGenre"] = logDAL.FavouriteGenre.ToString();
            return RedirectToAction("MyPage", "Movie");
           
        }
    }
}
