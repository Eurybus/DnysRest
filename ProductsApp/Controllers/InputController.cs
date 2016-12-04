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

            string updateQuery = "UPDATE " + DATABASE + "." + TABLE + " SET pic_href = @pic_href, url = @url, bio = @bio, password_hash = @password, salt = @salt WHERE nick = @nick;";
            string venueUpdateQuery = "SET foreign_key_checks = 0; UPDATE " + DATABASE + "." + TABLE + " SET venue_key = @venue WHERE nick = @nick; SET foreign_key_checks = 1";
            MySqlCommand updateCmd = new MySqlCommand(updateQuery + venueUpdateQuery, connect);
            

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
                    User inputUser = checkProperties(user);

                    updateCmd.Parameters.AddWithValue("@nick", inputUser.nick);
                    //updateCmd.Parameters.AddWithValue("@fname", user.fname);
                    //updateCmd.Parameters.AddWithValue("@lname", user.lname);
                    //updateCmd.Parameters.AddWithValue("@sex", user.sex);
                    //updateCmd.Parameters.AddWithValue("@age", user.age);
                    updateCmd.Parameters.AddWithValue("@pic_href", inputUser.avatar);
                    updateCmd.Parameters.AddWithValue("@url", inputUser.url);
                    updateCmd.Parameters.AddWithValue("@bio", inputUser.bio);
                    updateCmd.Parameters.AddWithValue("@venue", inputUser.venue);
                    updateCmd.Parameters.AddWithValue("@password", inputUser.password);
                    updateCmd.Parameters.AddWithValue("@salt", user.salt);

                    updateCmd.ExecuteNonQuery();
                    connect.Close();
                }
                else
                {
                    // User was not found in database, so let's create a new user
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
                // Modifying FK value (venue_key fails)
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
            MySqlConnection connect = new MySqlConnection(ConnStr);
            string nameCheckQuery = "SELECT * FROM " + DATABASE + "." + TABLE + " WHERE nick = @nick;";
            string idCheckQuery = "SELECT * FROM " + DATABASE + "." + TABLE + "WHERE id = @id;";

            string cmdQuery = "";
            bool usingID;
            if (user.nick == null)
            {
                cmdQuery = idCheckQuery;
                usingID = true;
            }
            else
            {
                cmdQuery = nameCheckQuery;
                usingID = false;
            }
            MySqlCommand cmd = new MySqlCommand(cmdQuery, connect);
            if (usingID)
                cmd.Parameters.AddWithValue("@id", user.Id);
            else
                cmd.Parameters.AddWithValue("@nick", user.nick);
            User referenceUser = new User();

            try
            {
                MySqlDataReader reader;
                connect.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    referenceUser.Id = reader.GetInt32(0);
                    referenceUser.nick = reader.GetString(1);
                    //referenceUser.fname = reader.GetString(2);
                    //referenceUser.lname = reader.GetString(3);
                    //referenceUser.sex = reader.GetBoolean(4);
                    //referenceUser.age = reader.GetInt32(5);
                    referenceUser.avatar = reader.GetString(6);
                    referenceUser.url = reader.GetString(7);
                    referenceUser.bio = reader.GetString(8);
                    /*if (reader.GetInt32(9) != 0)
                    {
                        referenceUser.venue = reader.GetInt32(9);

                    }*/
                    referenceUser.password = reader.GetString(10);
                    referenceUser.salt = reader.GetString(11);
                }
                reader.Close();
                connect.Close();
            }
            catch (Exception)
            {

                throw;
            }

            User output = user;
            /*if (output.nick == null)
                output.nick = referenceUser.nick;*/
            if (output.fname == null)
                output.fname = referenceUser.fname;
            if (output.lname == null)
                output.lname = referenceUser.lname;

            output.sex = referenceUser.sex;

            output.age = referenceUser.age;

            if (output.avatar == null)
                output.avatar = referenceUser.avatar;
            if (output.url == null)
                output.url = referenceUser.url;
            if (output.bio == null)
                output.bio = referenceUser.bio;

            if (output.password == null)
                output.password = referenceUser.password;
            if (output.salt == null)
                output.salt = referenceUser.salt;

            return output;

        }
    }
}