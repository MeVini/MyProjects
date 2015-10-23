using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
namespace Idbm.Models
{
   public class Comment
    {
       public String FirstName { get; set; }
       public String Comments { get; set; }
       public int Rating { get; set; }
    }
}
