using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ProductsApp
{
    public class AuthorizationModule
    {
        string ApiKeyKey = "ApiKey";
        /// <summary>
        /// Used for checking if the api key is valid
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Returns true if key is valid</returns>
        public bool checkAPIKey(string key)
        {
            string dummykey = "1337HAX";
            if (key == dummykey)
            {
                return true;
            }
            else
                return false;
        }
        public string getToken(HttpRequestMessage message)
        {
            try
            {
                var headers = message.Headers;
                string token = headers.GetValues(ApiKeyKey).First();
                return token;
            }
            catch (Exception)
            {
                return "no key";
            }
        }
    }
}