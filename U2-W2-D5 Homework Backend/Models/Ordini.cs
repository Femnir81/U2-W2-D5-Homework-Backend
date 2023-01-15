using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace U2_W2_D5_Homework_Backend.Models
{
    public class Ordini
    {
        public int ID { get; set; }
        [Display(Name = "Data Ordine")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataOrdine { get; set; }
        [Display(Name = "Quantità")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public int Quantita { get; set; }
        [Display(Name = "Prezzo")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public double PrezzoTotale { get; set; }
        [Display(Name = "Prenotazione")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public Prenotazione IDPrenotazione { get; set; }
        [Display(Name = "Servizio")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public Servizi IDServizi { get; set; }

        public static void AddOrdine(Ordini ordine, int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Insert into OrdiniTab values (@DataOrdine, @Quantita, @PrezzoTotale, @IDPrenotazione, @IDServizi)", con);
                command.Parameters.AddWithValue("@DataOrdine", ordine.DataOrdine);
                command.Parameters.AddWithValue("@Quantita", ordine.Quantita);
                command.Parameters.AddWithValue("@PrezzoTotale", ordine.PrezzoTotale);
                command.Parameters.AddWithValue("@IDPrenotazione", id);
                command.Parameters.AddWithValue("@IDServizi", ordine.IDServizi.ID);

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

        public static List<Ordini> GetOrdinibyServizio(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<Ordini> ListaOrdini = new List<Ordini>();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("SELECT OrdiniTab.IDPrenotazione, ServiziTab.Descrizione, OrdiniTab.DataOrdine, OrdiniTab.Quantita, OrdiniTab.PrezzoTotale FROM OrdiniTab INNER JOIN ServiziTab ON OrdiniTab.IDServizi = ServiziTab.ID WHERE OrdiniTab.IDPrenotazione = @ID", con);
                command.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Ordini ordine = new Ordini();
                        Servizi servizio = new Servizi();
                        ordine.IDServizi = servizio;
                        servizio.Descrizione = reader["Descrizione"].ToString();
                        ordine.DataOrdine = Convert.ToDateTime(reader["DataOrdine"]);
                        ordine.Quantita = Convert.ToInt32(reader["Quantita"]);
                        ordine.PrezzoTotale = Convert.ToDouble(reader["PrezzoTotale"]);
                        ListaOrdini.Add(ordine);
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
            return ListaOrdini;
        }

    }
}