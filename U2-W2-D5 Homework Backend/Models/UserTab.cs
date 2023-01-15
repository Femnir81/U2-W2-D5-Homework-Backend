using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc.Filters;
using System.Xml.Linq;

namespace U2_W2_D5_Homework_Backend.Models
{
    public class UserTab
    {
        public int ID { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Password { get; set; }
        [Display(Name = "Ruolo")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Roles { get; set; }

        public static bool UserAutenticato(string username, string password)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("Select * from UserTab where Username = @Username and [Password] = @Password", con);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally 
            { 
                con.Close(); 
            }

        }
    }
}