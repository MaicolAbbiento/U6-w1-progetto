using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services.Description;

namespace U6_w1_progetto.Models
{
    public class Verbale
    {
        public int Idverbale { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string Nominativo_Agente { get; set; }
        public DateTime DataTrascrizioneVerbale { get; set; }
        public decimal Importo { get; set; }
        public int DecurtamentoPunti { get; set; }
        public int Idpersona { get; set; }
        public int Idviolazione { get; set; }

        public void AddDb(Verbale verbale, string idepersonaCorrene, string idviolazion)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                "INSERT INTO verbale  VALUES (@DataViolazione, @IndirizzoViolazione, @Nominativo_Agente , @DataTrascrizioneVerbale, @Importo , @DecurtamentoPunti, @idviolazione , @idanagrafica )", conn);
                cmd.Parameters.AddWithValue("DataViolazione", verbale.DataViolazione);
                cmd.Parameters.AddWithValue("IndirizzoViolazione", verbale.IndirizzoViolazione);
                cmd.Parameters.AddWithValue("Nominativo_Agente", verbale.Nominativo_Agente);
                cmd.Parameters.AddWithValue("DataTrascrizioneVerbale ", verbale.DataTrascrizioneVerbale);
                cmd.Parameters.AddWithValue("Importo", verbale.Importo);
                cmd.Parameters.AddWithValue("DecurtamentoPunti", verbale.DecurtamentoPunti);
                cmd.Parameters.AddWithValue("idviolazione", Convert.ToInt32(idviolazion));
                cmd.Parameters.AddWithValue("idanagrafica", Convert.ToInt32(idepersonaCorrene));

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
        }

        public int Totverbali()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                int totalCount;
                string query = "SELECT COUNT(*) FROM verbale";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    totalCount = (int)command.ExecuteScalar();
                }

                return totalCount;
            }
            catch { return 0; }
            finally { conn.Close(); }
        }

        public List<Registoverbali> getverbaliPernome()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT Cognome, Nome, COUNT(*) AS ConteggioVerbaliAnagrafica FROM AnagraficaTrasgresorri  as Anagrafica INNER JOIN VERBALE  ON Anagrafica.IDAnagrafica = Verbale.IDAnagrafica GROUP BY Cognome, Nome;", conn);
                SqlDataReader sqlDataReader;
                conn.Open();
                List<Registoverbali> registoverbalis = new List<Registoverbali>();
                sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Registoverbali verbale = new Registoverbali(
                           (
                           sqlDataReader["cognome"]).ToString(),
                           sqlDataReader["nome"].ToString(),
                           Convert.ToInt32(sqlDataReader["ConteggioVerbaliAnagrafica"])
                        );
                    registoverbalis.Add(verbale);
                }
                return registoverbalis;
            }
            catch { return new List<Registoverbali>(); }
            finally { conn.Close(); }
        }

        public List<Registoverbali> getVerbaliPunti()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT Cognome, Nome, SUM(DecurtamentoPunti) AS PuntiDecurtatiAnagrafica FROM AnagraficaTrasgresorri  as Anagrafica INNER JOIN Verbale ON Anagrafica.IDAnagrafica = Verbale.IDAnagrafica GROUP BY Cognome, Nome;", conn);
                SqlDataReader sqlDataReader;
                conn.Open();
                List<Registoverbali> registoverbalis = new List<Registoverbali>();
                sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Registoverbali verbale = new Registoverbali(
                           (
                           sqlDataReader["cognome"]).ToString(),
                           sqlDataReader["nome"].ToString(),
                           Convert.ToInt32(sqlDataReader["PuntiDecurtatiAnagrafica"])
                        );
                    registoverbalis.Add(verbale);
                }
                return registoverbalis;
            }
            catch { return new List<Registoverbali>(); }
            finally { conn.Close(); }
        }

        public List<Registoverbali> getverbalisopra10Punti()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand(" SELECT Cognome, Nome, Indirizzo, DataViolazione, Importo, DecurtamentoPunti FROM AnagraficaTrasgresorri AS a INNER JOIN Verbale AS v ON a.IDAnagrafica = v.IDAnagrafica WHERE V.DecurtamentoPunti >= 10;", conn);
                SqlDataReader sqlDataReader;
                conn.Open();
                List<Registoverbali> registoverbalis = new List<Registoverbali>();
                sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Registoverbali verbale = new Registoverbali(
                           (
                           sqlDataReader["cognome"]).ToString(),
                           sqlDataReader["nome"].ToString(),
                           Convert.ToInt32(sqlDataReader["DecurtamentoPunti"]),
                           Convert.ToDecimal(sqlDataReader["Importo"])
                        );
                    registoverbalis.Add(verbale);
                }
                return registoverbalis;
            }
            catch { return new List<Registoverbali>(); }
            finally { conn.Close(); }
        }

        public List<Registoverbali> getverbalisopra400()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand(" SELECT Cognome, Nome, Indirizzo, DataViolazione, Importo, DecurtamentoPunti FROM AnagraficaTrasgresorri AS a INNER JOIN Verbale AS v ON a.IDAnagrafica = v.IDAnagrafica WHERE V.Importo >= 400;", conn);
                SqlDataReader sqlDataReader;
                conn.Open();
                List<Registoverbali> registoverbalis = new List<Registoverbali>();
                sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Registoverbali verbale = new Registoverbali(
                           (
                           sqlDataReader["cognome"]).ToString(),
                           sqlDataReader["nome"].ToString(),
                           Convert.ToInt32(sqlDataReader["DecurtamentoPunti"]),
                           Convert.ToDecimal(sqlDataReader["Importo"])
                        );
                    registoverbalis.Add(verbale);
                }
                return registoverbalis;
            }
            catch { return new List<Registoverbali>(); }
            finally { conn.Close(); }
        }
    }
}