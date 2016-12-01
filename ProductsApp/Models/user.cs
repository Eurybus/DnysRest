using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductsApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string nick { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public bool sex { get; set; }
        public int age { get; set; }
        public string avatar { get; set; }
        public string url { get; set; }
        public string bio { get; set; }
        public int venue { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
    }
}
