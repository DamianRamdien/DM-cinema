using System;

namespace DM_cinema.Database
{
    public class Film
    {
        public int Id { get; set; }

        public string Naam{ get; set; }

        public string Beschrijving { get; set; }

        public string Beschrijvingkort() { return this.Beschrijving.Substring(0, this.Beschrijving.IndexOf(" ", 200)); }

        public string Img { get; set; }

        public string Genre { get; set; }

        public string Datum { get; set; }

        public string Duur { get; set; } 
    }
}
