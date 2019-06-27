using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MovieApp.Manegers
{
    public class RegistrationManeger
    {
        string connectionString => ConfigurationManager.ConnectionStrings["MovieDB"].ConnectionString;

        public bool InsertRegistration(Registration reg)
        {
            string query = "Insert into MovieUser(UserName,Email,Password)values('" + reg.UserName + "','" + reg.Email + "','" + reg.Password + "')";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            int rowEffext = cmd.ExecuteNonQuery();
            con.Close();
            return rowEffext > 0;
        }
    }
}