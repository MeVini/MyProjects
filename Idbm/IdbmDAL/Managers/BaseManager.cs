using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace IdbmDAL.Managers
{
    public class BaseManager
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(BaseManager));
        public bool connection_open;
        public MySqlConnection connection;
        public MySqlCommand cmd = new MySqlCommand();
        public BaseManager()
        {
            Get_Connection();
            cmd.Connection = connection;
            //connection.Close();
        }
        private void Get_Connection()
        {
            connection_open = false;

            connection = new MySqlConnection();

            connection.ConnectionString = ConfigurationManager.ConnectionStrings["dvd_collectionEntities"].ConnectionString;


            if (Open_Local_Connection())
            {
                connection_open = true;
            }
            else
            {
            }

        }

        private bool Open_Local_Connection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                 logger.Error(ex.ToString());
                 return false;
            }
        }

    }
}
