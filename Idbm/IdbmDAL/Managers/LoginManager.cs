using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using Idbm.Models;
using System.Data;
using System.Data.SqlClient;

namespace IdbmDAL.Managers
{
    public class LoginManager : BaseManager
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(LoginManager));
        public Login1 RegisterAdmin(String FirstName, String LastName, String Email, String Password, String FavouriteGenre)
        {
            Login1 login1 = new Login1();
            try
            {

                MySqlCommand cmd = new MySqlCommand("Register", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@FavouriteGenre", FavouriteGenre);
                cmd.ExecuteNonQuery();

                login1.FirstName = FirstName;
                login1.LastName = LastName;
                login1.Email = Email;
                login1.Password = Password;
                login1.FavouriteGenre = FavouriteGenre;
            }



            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return login1;
        }
        public Login1 Registered(String Email, String Password)
        {
            MySqlCommand cmd = new MySqlCommand("CheckEmail", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MEmail", Email);
            cmd.Parameters.AddWithValue("@MPassword", Password);
            MySqlDataReader reader = cmd.ExecuteReader();
            Login1 login1 = new Login1();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    login1.Id = reader.GetInt32("Id");
                    login1.FirstName = reader.GetString("FirstName");
                    login1.LastName = reader.GetString("LastName");
                    login1.Email = reader.GetString("Email");
                    login1.Password = reader.GetString("Password");
                    login1.FavouriteGenre = reader.GetString("FavouriteGenre");
                }
                return login1;
            }
            else
            {
                reader.Dispose();
                cmd.Dispose();
                return null;
            }



        }
        public void SaveComments(String title, String message, int rate, int userid)
        {

            try
            {
                MySqlCommand cmd = new MySqlCommand("SaveComments", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Comments", message);
                cmd.Parameters.AddWithValue("@Rating", rate);
                cmd.Parameters.AddWithValue("@MovieTitle", title);
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }
        public List<Comment> GetCommentForMovieTitle(String title)
        {
            MySqlCommand cmd = new MySqlCommand("GetCommentForMovieTitle", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@title", title);
            List<Comment> CommentList = new List<Comment>();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Comment comment = new Comment();
                comment.FirstName = reader.GetString("FirstName");
                comment.Comments = reader.GetString("Comments");
                comment.Rating = reader.GetInt32("Rating");
                CommentList.Add(comment);
            }
            return CommentList;

        }
        public void ExternalLoginRegister(String FirstName, String LastName, String Email, String Password, String FavouriteGenre)
        {

            try
            {
                MySqlCommand cmd = new MySqlCommand("Register", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@FavouriteGenre", FavouriteGenre);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }
        public Login1 CheckExternalEmail(String Email)
        {
            MySqlCommand cmd = new MySqlCommand("CheckExternalEmail", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MEmail", Email);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                Login1 login1 = new Login1();
                if (reader.Read())
                {
                    login1.Id = reader.GetInt32("Id");
                    login1.FirstName = reader.GetString("FirstName");
                    login1.LastName = reader.GetString("LastName");
                    login1.Email = reader.GetString("Email");
                    login1.Password = reader.GetString("Password");
                    login1.FavouriteGenre = reader.GetString("FavouriteGenre");
                }
                return login1;
            }
            else
            {
                reader.Dispose();
                cmd.Dispose();
                return null;
            }
            }
           


        
    }
}
