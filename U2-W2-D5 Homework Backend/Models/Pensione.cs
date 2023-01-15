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
    public class Pensione
    {
        public int ID { get; set; }
        [Display(Name = "Pensione")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Tipologia { get; set; }

        public static List<Pensione> GetPensioni()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<Pensione> ListaPensioni = new List<Pensione>();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Select * from PensioniTab", con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Pensione pens = new Pensione();
                        pens.ID = Convert.ToInt32(reader["ID"]);
                        pens.Tipologia = reader["Tipologia"].ToString();
                        ListaPensioni.Add(pens);
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
            return ListaPensioni;
        }

        public static void CreatePensione(Pensione pens)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Insert into PensioniTab values (@Tipologia)", con);
                command.Parameters.AddWithValue("@Tipologia", pens.Tipologia);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
        }

        public static Pensione GetPensione(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            Pensione pens = new Pensione();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Select * from PensioniTab where ID = @ID", con);
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pens.ID = Convert.ToInt32(reader["ID"]);
                        pens.Tipologia = reader["Tipologia"].ToString();
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
            return pens;
        }

        public static void EditPensione(Pensione pens, int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Update PensioniTab set Tipologia = @Tipologia where ID = @ID", con);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@Tipologia", pens.Tipologia);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
        }

        public static void DeletePensione(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Delete from PensioniTab where ID = @ID", con);
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
        }

        public static List<SelectListItem> DropDownPensione()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<SelectListItem> DropDown = new List<SelectListItem>();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Select * from PensioniTab", con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SelectListItem item = new SelectListItem();
                        item.Value = reader["ID"].ToString();                       
                        item.Text = reader["Tipologia"].ToString();                  
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