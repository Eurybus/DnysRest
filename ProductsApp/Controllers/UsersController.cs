using ProductsApp.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductsApp.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        List<User> users = new List<User>();
        List<User> patrons = new List<User>();

        public MySqlConnection connect;

        private static string
            UID = "bc9d603639c5b1;",
            PASSWORD = "235001f2;",
            SERVER = "eu-cdbr-azure-north-e.cloudapp.net;",
            DATABASE = "dionys";

        private static string ConnStr = "server=" + SERVER + "database=" + DATABASE + "; user=" + UID + "password=" + PASSWORD;

/*        public UsersController()
        {
            System.Diagnostics.Debug.WriteLine("Konstruktorissa");
            connect = new MySqlConnection(ConnStr);
            connect.Open();
            FetchData(connect);
        }

        public void FetchData(MySqlConnection conn)
        {
            users.Clear();

            MySqlCommand cmd;
            string query = "Select * from dionys.users;";
            cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32(0),
                    nick = reader.GetString(1),
                    fname = reader.GetString(2),
                    lname = reader.GetString(3),
                    sex = reader.GetBoolean(4),
                    age = reader.GetInt32(5),
                    avatar = reader.GetString(6),
                    url = reader.GetString(7),
                    bio = reader.GetString(8),
                    venue = reader.GetInt32(9),
                    password = reader.GetString(10),
                    salt = reader.GetString(11),
                    

                });
            }
            reader.Close();
            conn.Close();
        }
*/
        public void FetchUsers(MySqlConnection conn, int id)
        {
            users.Clear();

            try
            {
                MySqlCommand cmd;
                string query = "Select * from dionys.users WHERE venue_key = " + id + ";";
                cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32(0),
                    nick = reader.GetString(1),
                    fname = reader.GetString(2),
                    lname = reader.GetString(3),
                    sex = reader.GetBoolean(4),
                    age = reader.GetInt32(5),
                    avatar = reader.GetString(6),
                    url = reader.GetString(7),
                    bio = reader.GetString(8),
                    venue = reader.GetInt32(9),
                    password = reader.GetString(10),
                    salt = reader.GetString(11),
                    

                });
            }
            reader.Close();
            conn.Close();

            }
            catch (MySqlException ex)
            {
                throw ex;
            }

        }

        [Route("")]
        public IEnumerable<User> GetAllUsers()
        {
            return users;
        }

        [HttpGet]
        //[ActionName("SelectVenue")]
        [Route("{venue:int}")]
        public List<User> SelectVenue(int venue)
        {
            connect = new MySqlConnection(ConnStr);
            connect.Open();
            FetchUsers(connect, venue);

            return users;
        }

        /*[Route("{id:int}")]
        //[ResponseType(typeof(user))]
        public IHttpActionResult GetVenue(int id)
        {
            var user = users.FirstOrDefault((p) => p.Id == id);

            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }*/
    }
}
