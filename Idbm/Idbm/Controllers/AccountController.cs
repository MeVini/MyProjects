using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Idbm.Filters;
using Idbm.Models;
using Facebook;

namespace Idbm.Controllers
{
   
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        [AllowAnonymous]
        public ActionResult login()
        {
            return View();
        }

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }

        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "887117078046213",
                client_secret = "d27adbc1c24cffa0fb4849592a3befd9",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email" // Add other permissions as needed
            });
          
            return Redirect(loginUrl.AbsoluteUri);
            //return View();
            
           
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "887117078046213",
                client_secret = "d27adbc1c24cffa0fb4849592a3befd9",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;

            // Store the access token in the session for farther use
            Session["AccessToken"] = accessToken;

            // update the facebook client with the access token so 
            // we can make requests on behalf of the user
            fb.AccessToken = accessToken;

            // Get the user's information
            dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
            string email = me.email;
            string firstname = me.first_name;
            string middlename = me.middle_name;
            string lastname = me.last_name;
            @Session["LogedUserName"] = me.first_name;
            @Session["LogedUserLastName"] = me.last_name;
            @Session["LogedUserEmail"] = me.email;
       
            
            // Set the auth cookie
            FormsAuthentication.SetAuthCookie(email, false);
          
            return RedirectToAction("CheckExternalLoginRegister", "Login");
        }
    }
}
