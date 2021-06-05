using DM_cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using DM_cinema.Database;


namespace DM_cinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Film> films= new List<Film>();
            films = GetFilms();

            return View(films);
        }
        
        private List<Film> GetFilms()
        {
            // stel in waar de database gevonden kan worden
            string connectionString = "Server=informatica.st-maartenscollege.nl;Database=110560;Uid=110560;Pwd=inf2021sql;";

            // maak een lege lijst waar we de namen in gaan opslaan
            List<Film> films = new List<Film>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand("select * from film", conn);

                // resultaat van de query lezen
                using (var reader = cmd.ExecuteReader())
                {
                    // elke keer een regel (of eigenlijk: database rij) lezen
                    while (reader.Read())
                    {
                        Film p = new Film
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Naam = reader["naam"].ToString(),
                            Beschrijving = reader["beschrijving"].ToString(),
                            Genre = reader["genre"].ToString(),
                            Datum = reader["datum"].ToString(),
                            Duur = reader["duur"].ToString(),
                        };
                        films.Add(p);
                    }
                }
            }
            // return de lijst met films
            return films;
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [Route("Films")]
        public IActionResult Films()
        {
            List<Film> films = new List<Film>();
            films = GetFilms();

            return View(films);
        }
        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [Route("Cinemas")]
        public IActionResult Cinemas()
        {
            return View();
        }
        [Route("Info Film")]
        public IActionResult InfoFilm()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
