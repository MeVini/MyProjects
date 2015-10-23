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


namespace Idbm.Controllers
{
    public class MovieController : Controller
    {
        //
        // GET: /Movie/

        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MovieController));
        Movie movieDAL = new Movie();
        Movies movie = new Movies();
        MovieManager movieManager = new MovieManager();

        public ActionResult DisplayView(int id)
        {
            try
            {
                movieDAL = movieManager.GetMovieById(id);
                movie = new Movies(movieDAL);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return View(movie);
        }
        public ActionResult DisplayTable()
        {
            List<Movie> movieList = new List<Movie>();
            try
            {
                movieList = movieManager.GetAllMovies();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return View(movieList);
        }
        public ActionResult FillView()
        {
            return View();
        }
       
        public ActionResult DisplayMovieByTitle(String title)
        {
            try
            {
                movieDAL = movieManager.GetMovieByTitle(title);
                movie = new Movies(movieDAL);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return View(movie);
        }

        public ActionResult DisplaySearchView(String title)
        {
            try
            {
                List<String> AllTitle = new List<String>();
                AllTitle = movieManager.GetMovieBySearchTitle(title);
                ViewBag.AllTitle = AllTitle;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return View();
        }

        [HttpPost]
        public JsonResult AutocompleteSuggestions(String term)
        {
            List<String> AllTitle = new List<String>();
            try
            {
                AllTitle = movieManager.GetMovieBySearchTitle(term);

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return Json(AllTitle, JsonRequestBehavior.AllowGet);

        }

        public ActionResult MyHomePage()
        {

            if (Session["AccessToken"] != null)
            {
                var fb = new Facebook.FacebookClient();
                string accessToken = Session["AccessToken"] as string;
                var logoutUrl = fb.GetLogoutUrl(new { access_token = accessToken, next = "http://localhost:54124/" });

                Session.RemoveAll();
                return Redirect(logoutUrl.AbsoluteUri);
            }
            @Session["LogedUserName"] = null;
            @Session["LogedUserGenre"] = null;
            @Session["LogedUserId"] = null;
            @Session["LogedUserLastName"] = null;
            @Session["LogedUserEmail"] = null;
            List<Movie> movieList = new List<Movie>();
            try
            {
                movieList = movieManager.GetAllMovies();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return View(movieList);

        }


        public ActionResult MyPage()
        {
            List<Movie> movieList = new List<Movie>();
            try
            {
                movieList = movieManager.GetAllMovies();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return View(movieList);

        }
        
       

    }
}
