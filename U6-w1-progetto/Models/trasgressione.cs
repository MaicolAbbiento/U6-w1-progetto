using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace U6_w1_progetto.Models
{
    public class Trasgressione
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Il Indirizzo è obbligatorio")]
        public string descrizione { get; set; }

        public Trasgressione()
        { }

        public Trasgressione(int id, string descrizione)
        {
            this.id = id;

            this.descrizione = descrizione;
        }

        public void addDb(Trasgressione p)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                "INSERT INTO tipoViolazione VALUES (@descrizione)", conn);
                cmd.Parameters.AddWithValue("descrizione", p.descrizione);
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

        public List<Trasgressione> GetTrasgressione()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("select * from tipoViolazione", conn);
                SqlDataReader sqlDataReader;
                conn.Open();
                List<Trasgressione> trasgressioni = new List<Trasgressione>();
                sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Trasgressione trasgressione = new Trasgressione(
                           Convert.ToInt32(sqlDataReader["Idviolazione"]),
                           sqlDataReader["descrizione"].ToString()
                        );
                    trasgressioni.Add(trasgressione);
                }
                return trasgressioni;
            }
            catch { return new List<Trasgressione>(); }
            finally { conn.Close(); }
        }
    }
}