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

        string connectionString = "Server=informatica.st-maartenscollege.nl;Database=110560;Uid=110560;Pwd=inf2021sql;";

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
                            Img = reader["img"].ToString(),
                            Genre = reader["genre"].ToString(),
                            Datum = reader["datum"].ToString(),
                            Duur = reader["duur"].ToString(),
                            Leeftijd = reader["leeftijd"].ToString(),
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

        [Route("Movies")]
        public IActionResult Movies()
        {
            List<Film> films = new List<Film>();
            films = GetFilms();

            return View(films);
        }

        [Route("Contact")]
        public IActionResult contact()
        {
            return View();
        }

        [Route("Contact")]
        [HttpPost]
        public IActionResult contact(PersonModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            SavePerson(model);

            return Redirect("/gelukt");
        }

        [Route("gelukt")]
        public IActionResult gelukt()
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

        [Route("films/{id}")]
        public IActionResult Films(string id)
        {
            var model = GetFilm(id);

            return View(model);
        }
        private Film GetFilm(string id)
        {

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from film where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Film p = new Film
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Naam = reader["naam"].ToString(),
                            Beschrijving = reader["beschrijving"].ToString(),
                            Img = reader["img"].ToString(),
                            Genre = reader["genre"].ToString(),
                            Datum = reader["datum"].ToString(),
                            Duur = reader["duur"].ToString(),
                            Leeftijd = reader["leeftijd"].ToString(),
                        };
                        return p;
                    }
                }
            }
            return null;
        }

        private void SavePerson(PersonModel person)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO customer(firstname, lastname, phonenumber, email, subject) VALUES(?firstname, ?lastname, ?phonenumber, ?email, ?subject)", conn);
                cmd.Parameters.Add("?firstname", MySqlDbType.VarChar).Value = person.firstname;
                cmd.Parameters.Add("?lastname", MySqlDbType.VarChar).Value = person.lastname;
                cmd.Parameters.Add("?phonenumber", MySqlDbType.VarChar).Value = person.phonenumber;
                cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = person.email;
                cmd.Parameters.Add("?subject", MySqlDbType.VarChar).Value = person.subject;
                cmd.ExecuteNonQuery();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
