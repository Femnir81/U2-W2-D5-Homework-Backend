using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace U2_W2_D5_Homework_Backend.Models
{
    public class Prenotazione
    {
        [Display(Name = "N° Prenotazione")]
        public int ID { get; set; }
        [Display(Name = "Data Prenotazione")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataPrenotazione { get; set; }
        [Display(Name = "Data Inizio Soggiorno")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataInizioSoggiorno { get; set; }
        [Display(Name = "Data Fine Soggiorno")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataFineSoggiorno { get; set; }
        [Display(Name = "Anno")]
        public string Anno { get; set; }
        [Display(Name = "Acconto")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public double Acconto { get; set; }
        [Display(Name = "Prezzo")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public double Prezzo { get; set; }
        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public Cliente IDCliente { get; set; }
        [Display(Name = "Camera")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public Camera IDCamera { get; set; }
        [Display(Name = "Tipo di Pensione")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public Pensione IDPensione { get; set; }

        public static List<Prenotazione> GetPrenotazioni()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<Prenotazione> ListaPrenotazioni = new List<Prenotazione>();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("SELECT PrenotazioniTab.ID, CamereTab.Numero, PrenotazioniTab.DataPrenotazione, PrenotazioniTab.DataInizioSoggiorno, PrenotazioniTab.DataFineSoggiorno, ClientiTab.Cognome, ClientiTab.Nome, ClientiTab.Cod_Fisc FROM PrenotazioniTab INNER JOIN ClientiTab ON PrenotazioniTab.ID = ClientiTab.ID INNER JOIN CamereTab ON PrenotazioniTab.ID = CamereTab.ID ORDER BY PrenotazioniTab.DataInizioSoggiorno DESC", con);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Prenotazione prenot = new Prenotazione();
                        Cliente client = new Cliente();
                        Camera stanza = new Camera();
                        prenot.IDCliente = client;
                        prenot.IDCamera = stanza;
                        prenot.ID = Convert.ToInt32(reader["ID"]);
                        prenot.DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]);
                        prenot.DataInizioSoggiorno = Convert.ToDateTime(reader["DataInizioSoggiorno"]);
                        prenot.DataFineSoggiorno = Convert.ToDateTime(reader["DataFineSoggiorno"]);
                        stanza.Numero = Convert.ToInt32(reader["Numero"]);
                        client.Cognome = reader["Cognome"].ToString();
                        client.Nome = reader["Nome"].ToString();
                        client.Cod_Fisc = reader["Cod_Fisc"].ToString();
                        ListaPrenotazioni.Add(prenot);
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
            return ListaPrenotazioni;
        }

        public static void CreatePrenotazione(Prenotazione prenot)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Insert into PrenotazioniTab values (@DataPrenotazione, @DataInizioSoggiorno, @DataFineSoggiorno, @Anno, @Acconto, @Prezzo, @IDCliente, @IDCamera, @IDPensione)", con);
                command.Parameters.AddWithValue("@DataPrenotazione", prenot.DataPrenotazione);
                command.Parameters.AddWithValue("@DataInizioSoggiorno", prenot.DataInizioSoggiorno);
                command.Parameters.AddWithValue("@DataFineSoggiorno", prenot.DataFineSoggiorno);
                command.Parameters.AddWithValue("@Anno", prenot.DataInizioSoggiorno.Year);
                command.Parameters.AddWithValue("@Acconto", prenot.Acconto);
                command.Parameters.AddWithValue("@Prezzo", prenot.Prezzo);
                command.Parameters.AddWithValue("@IDCliente", prenot.IDCliente.ID);
                command.Parameters.AddWithValue("@IDCamera", prenot.IDCamera.ID);
                command.Parameters.AddWithValue("@IDPensione", prenot.IDPensione.ID);

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

        public static Prenotazione GetPrenotazione(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            Prenotazione prenot = new Prenotazione();
            Camera stanza = new Camera();
            Pensione pens = new Pensione();
            prenot.IDCamera = stanza;
            prenot.IDPensione = pens;
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Select * from PrenotazioniTab where ID = @ID", con);
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        prenot.DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]);
                        prenot.DataInizioSoggiorno = Convert.ToDateTime(reader["DataInizioSoggiorno"]);
                        prenot.DataFineSoggiorno = Convert.ToDateTime(reader["DataFineSoggiorno"]);
                        prenot.Acconto = Convert.ToDouble(reader["Acconto"]);
                        prenot.Prezzo = Convert.ToDouble(reader["Prezzo"]);
                        prenot.IDCamera.ID = Convert.ToInt32(reader["IDCamera"]);
                        prenot.IDPensione.ID = Convert.ToInt32(reader["IDPensione"]);
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
            return prenot;
        }

        public static void EditPrenotazione(Prenotazione prenot, int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("Update PrenotazioniTab set DataPrenotazione = @DataPrenotazione, DataInizioSoggiorno = @DataInizioSoggiorno, DataFineSoggiorno = @DataFineSoggiorno, Anno = @Anno, Acconto = @Acconto, Prezzo = @Prezzo, IDCamera = @IDCamera, IDPensione = @IDPensione where ID = @ID", con);
                command.Parameters.AddWithValue("@ID", prenot.ID);
                command.Parameters.AddWithValue("@DataPrenotazione", prenot.DataPrenotazione);
                command.Parameters.AddWithValue("@DataInizioSoggiorno", prenot.DataInizioSoggiorno);
                command.Parameters.AddWithValue("@DataFineSoggiorno", prenot.DataFineSoggiorno);
                command.Parameters.AddWithValue("@Anno", prenot.DataInizioSoggiorno.Year);
                command.Parameters.AddWithValue("@Acconto", prenot.Acconto);
                command.Parameters.AddWithValue("@Prezzo", prenot.Prezzo);
                command.Parameters.AddWithValue("@IDCamera", prenot.IDCamera.ID);
                command.Parameters.AddWithValue("@IDPensione", prenot.IDPensione.ID);

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

        public static List<Prenotazione> GetPrenotazioneInfoCheckout(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<Prenotazione> ListaPrenotazione = new List<Prenotazione>();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("SELECT PrenotazioniTab.ID, CamereTab.Numero, ClientiTab.Cognome, ClientiTab.Nome, PrenotazioniTab.DataInizioSoggiorno, PrenotazioniTab.DataFineSoggiorno, PrenotazioniTab.Acconto, PrenotazioniTab.Prezzo FROM CamereTab INNER JOIN ClientiTab ON CamereTab.ID = ClientiTab.ID INNER JOIN PrenotazioniTab ON CamereTab.ID = PrenotazioniTab.IDCamera AND ClientiTab.ID = PrenotazioniTab.IDCliente WHERE PrenotazioniTab.ID = @ID", con);
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Prenotazione prenot = new Prenotazione();
                        Camera stanza = new Camera();
                        Cliente client = new Cliente();
                        prenot.IDCamera = stanza;
                        prenot.IDCliente = client;
                        prenot.ID = Convert.ToInt32(reader["ID"]);
                        prenot.DataInizioSoggiorno = Convert.ToDateTime(reader["DataInizioSoggiorno"]);
                        prenot.DataFineSoggiorno = Convert.ToDateTime(reader["DataFineSoggiorno"]);
                        prenot.Acconto = Convert.ToDouble(reader["Acconto"]);
                        prenot.Prezzo = Convert.ToDouble(reader["Prezzo"]);
                        prenot.IDCamera.Numero = Convert.ToInt32(reader["Numero"]);
                        prenot.IDCliente.Cognome = reader["Cognome"].ToString();
                        prenot.IDCliente.Nome = reader["Nome"].ToString();
                        ListaPrenotazione.Add(prenot);
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
            return ListaPrenotazione;
        }

        public static double GetCostoPrenotazioneCliente(int id)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            double totale = 0;
            double acconto = 0;
            double extra = 0;
            try
            {
                con.Open();
                SqlCommand command1 = ConnectionClass.GetCommand("Select * from PrenotazioniTab where ID = @ID", con);
                command1.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader1 = command1.ExecuteReader();

                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        Prenotazione prenot = new Prenotazione();
                        prenot.Prezzo = Convert.ToDouble(reader1["Prezzo"]);
                        prenot.Acconto = Convert.ToDouble(reader1["Acconto"]);
                        totale += prenot.Prezzo;
                        acconto += prenot.Acconto;
                    }
                }
                con.Close();

                con.Open();
                SqlCommand command2 = ConnectionClass.GetCommand("Select * from OrdiniTab where IDPrenotazione = @ID", con);
                command2.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader2 = command2.ExecuteReader();

                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        Ordini ordine = new Ordini();
                        ordine.PrezzoTotale = Convert.ToDouble(reader2["PrezzoTotale"]);
                        extra += ordine.PrezzoTotale;
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
            totale = totale - acconto + extra;
            return totale;
        }

        public static List<Prenotazione> GetPrenotazioniByCodFisc(string codicefiscale)
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<Prenotazione> ListaPrenotazioni = new List<Prenotazione>();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("SELECT PrenotazioniTab.ID, PrenotazioniTab.DataPrenotazione, PrenotazioniTab.DataInizioSoggiorno, PrenotazioniTab.DataFineSoggiorno, ClientiTab.Cognome, ClientiTab.Nome, ClientiTab.Cod_Fisc, CamereTab.Numero, PensioniTab.Tipologia FROM CamereTab INNER JOIN ClientiTab ON CamereTab.ID = ClientiTab.ID INNER JOIN PensioniTab ON CamereTab.ID = PensioniTab.ID INNER JOIN PrenotazioniTab ON CamereTab.ID = PrenotazioniTab.IDCamera where ClientiTab.Cod_Fisc = @Cod_Fisc", con);
                command.Parameters.AddWithValue("@Cod_Fisc", codicefiscale);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Prenotazione prenot = new Prenotazione();
                        Cliente client = new Cliente();
                        Camera stanza = new Camera();
                        Pensione pens = new Pensione();
                        prenot.IDCliente = client;
                        prenot.IDCamera = stanza;
                        prenot.IDPensione = pens;
                        prenot.ID = Convert.ToInt32(reader["ID"]);
                        prenot.DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]);
                        prenot.DataInizioSoggiorno = Convert.ToDateTime(reader["DataInizioSoggiorno"]);
                        prenot.DataFineSoggiorno = Convert.ToDateTime(reader["DataFineSoggiorno"]);
                        client.Cognome = reader["Cognome"].ToString();
                        client.Nome = reader["Nome"].ToString();
                        client.Cod_Fisc = reader["Cod_Fisc"].ToString();
                        stanza.Numero = Convert.ToInt32(reader["Numero"]);
                        pens.Tipologia = reader["Tipologia"].ToString();
                        ListaPrenotazioni.Add(prenot);
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
            return ListaPrenotazioni;
        }

        public static List<Prenotazione> GetPrenotazioniPensCompl()
        {
            SqlConnection con = ConnectionClass.GetConnectionDB();
            List<Prenotazione> ListaPrenotazioni = new List<Prenotazione>();
            try
            {
                con.Open();
                SqlCommand command = ConnectionClass.GetCommand("SELECT PrenotazioniTab.ID, PrenotazioniTab.DataPrenotazione, PrenotazioniTab.DataInizioSoggiorno, PrenotazioniTab.DataFineSoggiorno, ClientiTab.Cognome, ClientiTab.Nome, PensioniTab.Tipologia FROM ClientiTab INNER JOIN PensioniTab ON ClientiTab.ID = PensioniTab.ID INNER JOIN PrenotazioniTab ON ClientiTab.ID = PrenotazioniTab.IDCliente WHERE PensioniTab.Tipologia = @PensioneCompleta", con);
                command.Parameters.AddWithValue("@PensioneCompleta", "Pensione Completa");

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Prenotazione prenot = new Prenotazione();
                        Cliente client = new Cliente();
                        Pensione pens = new Pensione();
                        prenot.IDCliente = client;
                        prenot.IDPensione = pens;
                        prenot.ID = Convert.ToInt32(reader["ID"]);
                        prenot.DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]);
                        prenot.DataInizioSoggiorno = Convert.ToDateTime(reader["DataInizioSoggiorno"]);
                        prenot.DataFineSoggiorno = Convert.ToDateTime(reader["DataFineSoggiorno"]);
                        client.Cognome = reader["Cognome"].ToString();
                        client.Nome = reader["Nome"].ToString();
                        pens.Tipologia = reader["Tipologia"].ToString();
                        ListaPrenotazioni.Add(prenot);
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
            return ListaPrenotazioni;
        }
    }
}