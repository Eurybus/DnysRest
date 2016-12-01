using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductsApp.Controllers
{
    
    [RoutePrefix("api/input")]
    public class InputController : ApiController
    {
        string TABLE = "users";
        public MySqlConnection connect;

        private static string
            UID = "bc9d603639c5b1;",
            PASSWORD = "235001f2;",
            SERVER = "eu-cdbr-azure-north-e.cloudapp.net;",
            DATABASE = "dionys";

        private static string ConnStr = "server=" + SERVER + "database=" + DATABASE + "; user=" + UID + "password=" + PASSWORD;

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string key, [FromBody]string value, [FromBody]string identifier, [FromBody]string nick)
        {
            string nickActual = "'" + nick + "'";
            string updateQuery = "UPDATE " + DATABASE + "." + TABLE + " SET " + key + " = " + value + "WHERE " + identifier + " = " + nickActual + ";";
            MySqlConnection connect = new MySqlConnection(ConnStr);
            MySqlCommand cmd = new MySqlCommand(updateQuery, connect);

            try
            {
                connect.Open();
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception)
            {
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}