using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;

namespace U2_W2_D5_Homework_Backend.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        [Display(Name = "Nome")]
        [Required (ErrorMessage = "Il campo è obbligatorio")]
        public string Nome { get; set; }
        [Display(Name = "Cognome")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Cognome { get; set; }
        [Display(Name = "Codice Fiscale")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il campo deve contenere 16 caratteri")]
        public string Cod_Fisc { get; set; }
        [Display(Name = "Città")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Citta { get; set; }
        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Il campo deve contenere 2 caratteri")]
        public string Provincia { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Email { get; set; }
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }
        [Display(Name = "Cellulare")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Cellulare { get; set; }

        public static List<Cliente> GetClienti()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<Cliente> ListaClienti = new List<Cliente>();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Select * from ClientiTab", con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Cliente client = new Cliente();
                        client.ID = Convert.ToInt32(reader["ID"]);
                        client.Nome = reader["Nome"].ToString();
                        client.Cognome = reader["Cognome"].ToString();
                        client.Cod_Fisc = reader["Cod_Fisc"].ToString();
                        client.Citta = reader["Citta"].ToString();
                        client.Provincia = reader["Provincia"].ToString();
                        client.Email = reader["Email"].ToString();
                        if (reader["Telefono"] == DBNull.Value)
                        {
                            client.Telefono = null;
                        }
                        else
                        {
                            client.Telefono = reader["Telefono"].ToString();                           
                        }
                        client.Cellulare = reader["Cellulare"].ToString();
                        ListaClienti.Add(client);   
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
            return ListaClienti;
        }

        public static void CreateCliente(Cliente client)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Insert into ClientiTab values (@Nome, @Cognome, @Cod_Fisc, @Citta, @Provincia, @Email, @Telefono, @Cellulare)", con);
                command.Parameters.AddWithValue("@Nome", client.Nome);
                command.Parameters.AddWithValue("@Cognome", client.Cognome);
                command.Parameters.AddWithValue("@Cod_Fisc", client.Cod_Fisc);
                command.Parameters.AddWithValue("@Citta", client.Citta);
                command.Parameters.AddWithValue("@Provincia", client.Provincia);
                command.Parameters.AddWithValue("@Email", client.Email);
                if (client.Telefono == null)
                {
                    command.Parameters.AddWithValue("@Telefono", "");
                }
                else
                {
                    command.Parameters.AddWithValue("@Telefono", client.Telefono);

                }
                command.Parameters.AddWithValue("@Cellulare", client.Cellulare);

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

        public static Cliente GetCliente(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            Cliente client = new Cliente();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Select * from ClientiTab where ID = @ID", con);
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        client.ID = Convert.ToInt32(reader["ID"]);
                        client.Nome = reader["Nome"].ToString();
                        client.Cognome = reader["Cognome"].ToString();
                        client.Cod_Fisc = reader["Cod_Fisc"].ToString();
                        client.Citta = reader["Citta"].ToString();
                        client.Provincia = reader["Provincia"].ToString();
                        client.Email = reader["Email"].ToString();
                        if (reader["Telefono"] == DBNull.Value)
                        {
                            client.Telefono = null;
                        }
                        else
                        {
                            client.Telefono = reader["Telefono"].ToString();
                        }
                        client.Cellulare = reader["Cellulare"].ToString();
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
            return client;
        }

        public static Cliente EditCliente(Cliente client, int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Update ClientiTab set Nome = @Nome, Cognome = @Cognome, Cod_Fisc = @Cod_Fisc, Citta = @Citta, Provincia = @Provincia, Email = @Email, Telefono = @Telefono, Cellulare = @Cellulare where ID = @ID", con);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@Nome", client.Nome);
                command.Parameters.AddWithValue("@Cognome", client.Cognome);
                command.Parameters.AddWithValue("@Cod_Fisc", client.Cod_Fisc);
                command.Parameters.AddWithValue("@Citta", client.Citta);
                command.Parameters.AddWithValue("@Provincia", client.Provincia);
                command.Parameters.AddWithValue("@Email", client.Email);
                if (client.Telefono == null)
                {
                    command.Parameters.AddWithValue("@Telefono", "");
                }
                else
                {
                    command.Parameters.AddWithValue("@Telefono", client.Telefono);

                }
                command.Parameters.AddWithValue("@Cellulare", client.Cellulare);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return client;
        }

        public static void DeleteClient(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Delete from ClientiTab where ID = @ID", con);
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

        public static List<SelectListItem> DropDownCliente()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<SelectListItem> DropDown = new List<SelectListItem>();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Select * from ClientiTab", con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SelectListItem item = new SelectListItem();
                        item.Value = reader["ID"].ToString();
                        item.Text = reader["Cognome"].ToString() + " " + reader["Nome"].ToString();
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