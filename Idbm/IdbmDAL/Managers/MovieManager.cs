using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using Idbm.Models;
namespace IdbmDAL.Managers
{
    public class MovieManager : BaseManager
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MovieManager));
        /// <summary>
        /// Function to search movie by ID
        /// </summary>
        /// <param name="Mid">ID of the movies to be searched</param>
        /// <returns>List of movies whose ID matches with user input</returns>
        public Movie GetMovieById(int Mid)
        {
            MySqlCommand cmd = new MySqlCommand("GetMovieId", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mid", Mid);
            MySqlDataReader reader = cmd.ExecuteReader();
            Movie movie = null;

            if (reader.Read())
            {
                try
                {
                   movie = new Movie();
                   movie.Id = reader.GetInt32("Id");
                   movie.Title = reader.GetString("Title");
                   movie.Description = reader.GetString("Description");
                   movie.ReleaseDate = reader.GetDateTime("ReleaseDate");
                   movie.Genre = reader.GetString("Genre");
                   movie.Review = reader.GetString("Review");
                   movie.Image = (byte[])(reader["Image"]);
                   movie.Url = reader.GetString("Url");
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                }

            }

            return movie;
        }
        /// <summary>
        /// Function to get all movies from the database
        /// </summary>
        /// <returns>List of all movies present in the database</returns>
        public List<Movie> GetAllMovies()
        {
            MySqlCommand cmd = new MySqlCommand("GetAllMovies", connection);
            List<Movie> MovieList = new List<Movie>();
            MySqlDataReader reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {

                    Movie movie = new Movie();
                    movie.Id = reader.GetInt32("Id");
                    movie.Title = reader.GetString("Title");
                    movie.Description = reader.GetString("Description");
                    movie.ReleaseDate = reader.GetDateTime("ReleaseDate");
                    movie.Genre = reader.GetString("Genre");
                    movie.Review = reader.GetString("Review");
                    movie.Image = (byte[])(reader["Image"]);
                    movie.Url = reader.GetString("Url");
                    MovieList.Add(movie);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            finally
            {
                reader.Close();
            }
            return MovieList;
        }

        /// <summary>
        /// Function to search movies by title
        /// </summary>
        /// <param name="title">Title of the movies to be searched</param>
        /// <returns>List of movies which titles matches with user input</returns>
        public Movie GetMovieByTitle(String Mtitle)
        {
            MySqlCommand cmd = new MySqlCommand("GetMovieByTitle", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mtitle", Mtitle);
            MySqlDataReader reader = cmd.ExecuteReader();
            Movie movie = null;

            if (reader.Read())
            {
                try
                {
                    movie = new Movie();
                    movie.Id = reader.GetInt32("Id");
                    movie.Title = reader.GetString("Title");
                    movie.Description = reader.GetString("Description");
                    movie.ReleaseDate = reader.GetDateTime("ReleaseDate");
                    movie.Genre = reader.GetString("Genre");
                    movie.Review = reader.GetString("Review");
                    movie.Image = (byte[])(reader["Image"]);
                    movie.Url = reader.GetString("Url");
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                }
            }

            return movie;
        }


        /// <summary>
        /// Function to search movies by title
        /// </summary>
        /// <param name="title">Title of the movies to be searched</param>
        /// <returns>List of movies which titles matches with user input</returns>
        public List<String> GetMovieBySearchTitle(String title)
        {
            MySqlCommand cmd = new MySqlCommand("GetMovieBySearchTitle", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mtitle", title);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<String> Title = new List<String>();
            try
            {
                while (reader.Read())
                {
                    Title.Add(reader.GetString("Title"));
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return Title;
        }


    }
}
