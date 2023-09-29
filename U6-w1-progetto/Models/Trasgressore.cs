using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace U6_w1_progetto.Models
{
    public class Trasgressore
    {
        public int Idanagrafica { get; set; }

        [Required(ErrorMessage = "Il Cognome è obbligatorio")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il Indirizzo è obbligatorio")]
        public string Indirizzo { get; set; }

        [Required(ErrorMessage = "Il Città è obbligatorio")]
        public string Città { get; set; }

        [Required(ErrorMessage = "Il CAP è obbligatorio")]
        public string CAP { get; set; }

        [Required(ErrorMessage = "Il Cod_Fisc è obbligatorio")]
        public string Cod_Fisc { get; set; }

        public string errore { get; set; }

        public Trasgressore(int idangrafica, string cognome, string nome, string indirizzo, string citta, string cap, string cod_Fisc)
        {
            Idanagrafica = idangrafica;
            Cognome = cognome;
            Nome = nome;
            Indirizzo = indirizzo;
            Città = citta;
            CAP = cap;
            Cod_Fisc = cod_Fisc;
        }

        public Trasgressore()
        { }

        public void addDb(Trasgressore trasgressore)
        {
            List<Trasgressore> t = GetTrasgressione();
            Trasgressore b = t.Find((a) => a.Cod_Fisc == trasgressore.Cod_Fisc);
            if (b == null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(connectionString);
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                    "INSERT INTO AnagraficaTrasgresorri  VALUES (@Cognome, @Nome, @Indirizzo , @Città , @CAP, @Cod_Fisc)", conn);
                    cmd.Parameters.AddWithValue("Cognome", trasgressore.Cognome);
                    cmd.Parameters.AddWithValue("Nome", trasgressore.Nome);
                    cmd.Parameters.AddWithValue("Indirizzo", trasgressore.Indirizzo);
                    cmd.Parameters.AddWithValue("Città ", trasgressore.Città);
                    cmd.Parameters.AddWithValue("CAP", trasgressore.CAP);
                    cmd.Parameters.AddWithValue("Cod_Fisc", trasgressore.Cod_Fisc);
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
            else
            {
                errore = "elemento gia presente";
            }
        }

        public List<Trasgressore> GetTrasgressione()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("select * from AnagraficaTrasgresorri", conn);
                SqlDataReader sqlDataReader;
                conn.Open();
                List<Trasgressore> trasgressioni = new List<Trasgressore>();
                sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Trasgressore trasgressione = new Trasgressore(
                           Convert.ToInt32(sqlDataReader["idanagrafica"]),
                           sqlDataReader["cognome"].ToString(),
                           sqlDataReader["nome"].ToString(),
                           sqlDataReader["indirizzo"].ToString(),
                           sqlDataReader["città"].ToString(),
                           sqlDataReader["cap"].ToString(),
                           sqlDataReader["Cod_Fisc"].ToString()
                        );
                    trasgressioni.Add(trasgressione);
                }
                return trasgressioni;
            }
            catch { return new List<Trasgressore>(); }
            finally { conn.Close(); }
        }
    }
}