using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Services.Communication
{
    public class GreenhouseIPRetriever
    {

        public string GetIP(string greenhouseID)
        {
            //TODO
            /*
            string databaseConnectionString = "";//TODO databaseconnection string
            SqlConnection databaseConnection = new SqlConnection(databaseConnectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM greenhouse WHERE greenhouse_id = " + greenhouseID);

            SqlDataReader reader = command.ExecuteReader();
            string greenhouseIP = reader.GetString(3);
            int greenhousePort = reader.GetInt32(4);

            return greenhouseIP + ":" + greenhousePort;
            */

            return "127.0.0.1" + ":" + "8080";  //DUMMY CONNECTION
        }


    }
}