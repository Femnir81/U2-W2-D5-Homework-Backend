using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace U2_W2_D5_Homework_Backend.Models
{
    public class ConnectionClass
    {
        public static SqlConnection GetConnectionDB()
        {
            //string constring = ConfigurationManager.ConnectionStrings["AlbergoDB"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AlbergoDB"].ToString());
            return con;
        }

        public static SqlCommand GetCommand(string commandtext, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(commandtext, connection);
            return command;
        }
    }
}