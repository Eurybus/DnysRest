//deployment credentials: un: dionysdev pw: 0Blivion


using ProductsApp.Models;
using System;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Diagnostics;
using System.Web.Http.Description;

namespace ProductsApp.Controllers
{
    [RoutePrefix("api/venues")]
    public class VenuesController : ApiController
    {

        List<User> users = new List<User>();
        List<Venue> venues = new List<Venue>();


        /*Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M },
            new Product { Id = 4, Name = "Leka", Category = "Rautaa", Price = 32.55M },
            new Product { Id = 5, Name = "Error field", Category = "Ebin", Price = 404 }
        };*/

        public MySqlConnection connect;

        private static string
            UID = "bc9d603639c5b1;",
            PASSWORD = "235001f2;",
            SERVER = "eu-cdbr-azure-north-e.cloudapp.net;",
            DATABASE = "dionys";

        private static string ConnStr = "server=" + SERVER + "database=" + DATABASE + "; user=" + UID + "password=" + PASSWORD;
    
        public VenuesController()
        {
            try
            {
                connect = new MySqlConnection(ConnStr);
                connect.Open();
                FetchData(connect);
            }
            catch (MySqlException ex)
            {

                throw ex;
            }
        }

        public void FetchData(MySqlConnection conn)
        {
           /* MySqlCommand cmd;
            string query = "Select * from dionys.users;";
            cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new user
                {
                    nick = reader.GetString(1),
                    fname = reader.GetString(2),
                    lname = reader.GetString(3),
                    sex = reader.GetBoolean(4),
                    age = reader.GetInt32(5),
                    avatar = reader.GetString(6),
                    url = reader.GetString(7),
                    bio = reader.GetString(8),
                    password = reader.GetString(9),
                    salt = reader.GetString(10),
                    Id = reader.GetInt32(0),

                });
            }

            reader.Close();
            conn.Close();
            */

            MySqlCommand cmd;
            string query = "Select * from dionys.venues;";
            cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                venues.Add(new Venue
                {                 
                     Id = reader.GetInt16(0),
                     name = reader.GetString(1),
                     address = reader.GetString(2),
                     lati = reader.GetDouble(3),
                     longi = reader.GetDouble(4),
                     desc = reader.GetString(5),
                });
            }

            reader.Close();
            conn.Close();

        }

/*public IEnumerable<Product> GetAllProducts() {
return products;
}*/

        [Route("")]
        public IEnumerable<Venue> GetAllVenues()
        {
            return venues;
        }

        /* public IHttpActionResult GetProduct(int id) {

             var product = products.FirstOrDefault((p) => p.Id == id);
             if (product == null)
             {
                 return NotFound();
             }
             return Ok(product);
         }*/

        [Route("{id:int}")]
        //[ResponseType(typeof(user))]
        public IHttpActionResult GetVenue(int id)
        {
            var venue = venues.FirstOrDefault((p) => p.Id == id);

            if(venue == null)
            {
                return NotFound();
            }
            return Ok(venue);
        }

    }
}
