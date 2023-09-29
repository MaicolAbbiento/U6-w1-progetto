using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace U6_w1_progetto.Models
{
    public class Registoverbali
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public int nViolazioni { get; set; }
        public decimal importo { get; set; }
        public List<Registoverbali> Ristorerai { get; set; }

        public Registoverbali()
        { }

        public Registoverbali(string nome, string cognome, int nViolazioni)
        {
            Nome = nome;
            Cognome = cognome;
            this.nViolazioni = nViolazioni;
        }
        public Registoverbali(string nome, string cognome, int nViolazioni, decimal importo) 
        {
            Nome = nome;
            Cognome = cognome;
            this.nViolazioni = nViolazioni;
            this.importo = importo; 
        }   
    }
}