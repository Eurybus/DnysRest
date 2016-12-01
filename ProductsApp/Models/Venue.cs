using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string desc { get; set; }
        public double lati { get; set; }
        public double longi { get; set; }      
    }
}