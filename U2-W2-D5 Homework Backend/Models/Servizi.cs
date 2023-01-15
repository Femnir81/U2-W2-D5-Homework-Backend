using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace U2_W2_D5_Homework_Backend.Models
{
    public class Servizi
    {
        public int ID { get; set; }
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }

        public static List<SelectListItem> DropDownServizi()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<SelectListItem> DropDown = new List<SelectListItem>();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Select * from ServiziTab", con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SelectListItem item = new SelectListItem();
                        item.Value = reader["ID"].ToString();
                        item.Text = reader["Descrizione"].ToString();
                        DropDown.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return DropDown;
        }
    }
}