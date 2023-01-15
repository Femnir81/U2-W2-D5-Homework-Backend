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
    public class Camera
    {
        public int ID { get; set; }
        [Display(Name = "N° Stanza")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public int Numero { get; set; }
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }
        [Display(Name = "Doppia")]
        public bool Doppia { get; set; }
        [Display(Name = "Disponibilità")]
        public bool Disponibilita { get; set; }

        public static List<Camera> GetCamere()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<Camera> ListaCamere = new List<Camera>();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Select * from CamereTab order by Numero asc", con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Camera stanza = new Camera();
                        stanza.ID = Convert.ToInt32(reader["ID"]);
                        stanza.Numero = Convert.ToInt32(reader["Numero"]);
                        if (reader["Descrizione"] == DBNull.Value)
                        {
                            stanza.Descrizione = "";
                        }
                        else
                        {
                            stanza.Descrizione = reader["Descrizione"].ToString();
                        }
                        stanza.Doppia = Convert.ToBoolean(reader["Doppia"]);
                        stanza.Disponibilita = Convert.ToBoolean(reader["Disponibilita"]);
                        ListaCamere.Add(stanza);
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
            return ListaCamere;
        }

        public static void CreateCamera(Camera stanza)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Insert into CamereTab values (@Numero, @Descrizione, @Doppia, @Disponibilita)", con);
                command.Parameters.AddWithValue("@Numero", stanza.Numero);
                if (stanza.Descrizione == null)
                {
                    command.Parameters.AddWithValue("@Descrizione", "");
                }
                else
                {
                command.Parameters.AddWithValue("@Descrizione", stanza.Descrizione);
                }
                command.Parameters.AddWithValue("@Doppia", stanza.Doppia);
                command.Parameters.AddWithValue("@Disponibilita", stanza.Disponibilita);

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

        public static Camera GetCamera(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            Camera stanza = new Camera();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Select * from CamereTab where ID = @ID", con);
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        stanza.ID = Convert.ToInt32(reader["ID"]);
                        stanza.Numero = Convert.ToInt32(reader["Numero"]);
                        if (reader["Descrizione"] == DBNull.Value)
                        {
                            stanza.Descrizione = "";
                        }
                        else
                        {
                            stanza.Descrizione = reader["Descrizione"].ToString();
                        }
                        stanza.Doppia = Convert.ToBoolean(reader["Doppia"]);
                        stanza.Disponibilita = Convert.ToBoolean(reader["Disponibilita"]);
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
            return stanza;
        }

        public static void EditCamera(Camera stanza, int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Update CamereTab set Numero = @Numero, Descrizione = @Descrizione, Doppia = @Doppia, Disponibilita = @Disponibilita where ID = @ID", con);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@Numero", stanza.Numero);
                if (stanza.Descrizione == null)
                {
                    command.Parameters.AddWithValue("@Descrizione", "");
                }
                else
                {
                    command.Parameters.AddWithValue("@Descrizione", stanza.Descrizione);
                }
                command.Parameters.AddWithValue("@Doppia", stanza.Doppia);
                command.Parameters.AddWithValue("@Disponibilita", stanza.Disponibilita);

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

        public static void DeleteCamera(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Delete from CamereTab where ID = @ID", con);
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

        public static List<SelectListItem> DropDownCamera()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<SelectListItem> DropDown = new List<SelectListItem>();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Select * from CamereTab where Disponibilita = 1", con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SelectListItem item = new SelectListItem();
                        item.Value = reader["ID"].ToString();
                        if (Convert.ToBoolean(reader["Doppia"]))
                        {
                            item.Text = "Stanza n° " + reader["Numero"].ToString() + " - Camera Doppia";
                        }
                        else
                        {
                            item.Text = "Stanza n° " + reader["Numero"].ToString() + " - Camera Singola";
                        }
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