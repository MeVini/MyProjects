using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using Idbm.LocalResource;


namespace Idbm.Models
{
    public class Logins
    {
        public int Id { get; set; }

        [Display(Name="FirstName",ResourceType=typeof(Resource))]
        [RegularExpression("[A-Za-z ]*", ErrorMessageResourceName = "NameError", ErrorMessageResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "PleaseenteryourFirstName", ErrorMessageResourceType = typeof(Resource))]
        public String FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resource))]
        [RegularExpression("[A-Za-z ]*", ErrorMessageResourceName = "NameError", ErrorMessageResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "PleaseenteryourLastName", ErrorMessageResourceType = typeof(Resource))]
        public String LastName { get; set; }

        [Display(Name="Email",ResourceType=typeof(Resource))]
        [Required(ErrorMessageResourceName = "PleaseenteryourvalidEmailId", ErrorMessageResourceType = typeof(Resource))]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessageResourceName = "Pleaseenteryourvalidemailaddress",ErrorMessageResourceType = typeof(Resource))]
        public String Email { get; set; }

        [Display(Name = "Password",ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Thepasswordfieldisrequired", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6 )]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resource))]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "Thepasswordfieldisrequired", ErrorMessageResourceType = typeof(Resource))]
        [CompareAttribute("Password", ErrorMessageResourceName = "Thepasswordandconfirmationpassworddonotmatch", ErrorMessageResourceType = typeof(Resource))]
        public String ConfirmPassword { get; set; }

        [Display(Name = "FavouriteGenre", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "Selectanoption", ErrorMessageResourceType = typeof(Resource))]
        public String FavouriteGenre { get; set; }

        public Logins()
        {
        }
        public Logins(Login1 log)
        {
            Id = log.Id;
            FirstName = log.FirstName;
            LastName = log.LastName;
            Password = log.Password;
           
        }
    }
     
}