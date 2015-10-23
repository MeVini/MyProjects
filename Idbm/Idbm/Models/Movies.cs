using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
namespace Idbm.Models
{

    public class Movies
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string Review { get; set; }
        public byte[] Image { get; set; }
        public string Url { get; set; }

        [Display(Name="Comments")]
        public string message { get; set; }
        

        public Movies()
        {
        }

        #region Functions

        public Movies(Movie movie)
        {
            Id = movie.Id;
            Title = movie.Title;
            Description = movie.Description;
            ReleaseDate = movie.ReleaseDate;
            Genre = movie.Genre;
            Review = movie.Review;
            Image = movie.Image;
            Url = movie.Url;
        }

        #endregion

    }
}