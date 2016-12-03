using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductsApp.Models;

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
        public void Post([FromBody]User user)
        {
            MySqlConnection connect = new MySqlConnection(ConnStr);
            string nameCheckQuery = "SELECT id FROM " + DATABASE + "." + TABLE + " Where nick = @nick;";
            MySqlCommand nameCheckCmd = new MySqlCommand(nameCheckQuery, connect);
            nameCheckCmd.Parameters.AddWithValue("@nick", user.nick);

            string updateQuery = "UPDATE " + DATABASE + "." + TABLE + " SET nick = @nick, fname = @fname, lname = @lname, sex = @sex, age = @age, pic_href = @pic_href, url = @url, bio = @bio, venue_key = @venue, password_hash = @password, salt = @salt;";
            MySqlCommand updateCmd = new MySqlCommand(updateQuery, connect);
            

            string insertQuery = "INSERT INTO dionys.users(nick,fname,lname,sex,age,pic_href,url,bio,venue_key,password_hash,salt)";
            string insertValues = "VALUES(@nick, @fname, @lname, @sex, @age, @pic_href, @url, @bio, @venue, @password, @salt);";
            MySqlCommand insertCmd = new MySqlCommand(insertQuery + insertValues);
            try
            {
                MySqlDataReader reader;
                connect.Open();
                int id = 0;
                reader = nameCheckCmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                reader.Close();
                if(id != 0)
                {
                    updateCmd.Parameters.AddWithValue("@nick", user.nick);
                    updateCmd.Parameters.AddWithValue("@fname", user.fname);
                    updateCmd.Parameters.AddWithValue("@lname", user.lname);
                    updateCmd.Parameters.AddWithValue("@sex", user.sex);
                    updateCmd.Parameters.AddWithValue("@age", user.age);
                    updateCmd.Parameters.AddWithValue("@pic_href", user.avatar);
                    updateCmd.Parameters.AddWithValue("@url", user.url);
                    updateCmd.Parameters.AddWithValue("@bio", user.bio);
                    updateCmd.Parameters.AddWithValue("@venue", user.venue);
                    updateCmd.Parameters.AddWithValue("@password", user.password);
                    updateCmd.Parameters.AddWithValue("@salt", user.salt);

                    updateCmd.ExecuteNonQuery();
                    connect.Close();
                }
                else
                {
                    // User was not found in database, so let's create it
                    insertCmd.Parameters.AddWithValue("@nick", user.nick);
                    insertCmd.Parameters.AddWithValue("@fname", user.fname);
                    insertCmd.Parameters.AddWithValue("@lname", user.lname);
                    insertCmd.Parameters.AddWithValue("@sex", user.sex);
                    insertCmd.Parameters.AddWithValue("@age", user.age);
                    insertCmd.Parameters.AddWithValue("@pic_href", user.avatar);
                    insertCmd.Parameters.AddWithValue("@url", user.url);
                    insertCmd.Parameters.AddWithValue("@bio", user.bio);
                    insertCmd.Parameters.AddWithValue("@venue", user.venue);
                    insertCmd.Parameters.AddWithValue("@password", user.password);
                    insertCmd.Parameters.AddWithValue("@salt", user.salt);

                    insertCmd.ExecuteNonQuery();
                    connect.Close();
                }
            }
            catch (MySqlException ex)
            {

                throw ex;
            }

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

        private User checkProperties(User user)
        {
            User output = user;
            if (output.nick == null)
                output.nick = "";
            return output;

        }
    }
}