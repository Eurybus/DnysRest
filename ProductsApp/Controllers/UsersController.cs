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

        private AuthorizationModule authmod;

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
        public void FetchPatrons(MySqlConnection conn, int id)
        {
            patrons.Clear();

            try
            {
                MySqlCommand cmd;
                string query = "Select * from dionys.users WHERE venue_key = " + id + ";";
                cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                patrons.Add(new User
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
                    //password = reader.GetString(10),
                    //salt = reader.GetString(11),
                    

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

        public void FetchAllUsers(MySqlConnection conn)
        {
            users.Clear();

            try
            {
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
                        //password = reader.GetString(10),
                        //salt = reader.GetString(11),


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
            authmod = new AuthorizationModule();
            string token = authmod.getToken(this.Request);
            if(authmod.checkAPIKey(token))
            {
                connect = new MySqlConnection(ConnStr);
                connect.Open();
                FetchAllUsers(connect);
                return users;
            }
            else
            {
                List<User> fail = new List<User>();
                return fail;
            }
            
        }

        [HttpGet]
        //[ActionName("SelectVenue")]
        [Route("{venue:int}")]
        public List<User> SelectVenue(int venue)
        {
            authmod = new AuthorizationModule();
            string token = authmod.getToken(this.Request);
            if (authmod.checkAPIKey(token))
            {
                connect = new MySqlConnection(ConnStr);
                connect.Open();
                FetchPatrons(connect, venue);

                return patrons; 
            }
            else
            {
                List<User> fail = new List<User>();
                return fail;
            }
        }

        
    }
}
