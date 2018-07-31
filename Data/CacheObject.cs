using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data

{

    public class CacheObject
    {
        public AppConfig AppConfig { get; set; }
        public bool UseDbForPrices { get; set; }
        public User User { get; set; }
        public List<Products> Products { get; set; }
     
        
    }




    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

    }
    public class AppConfig
    {
        public string CdnUrl { get; set; }
        public string ApiUrl { get; set; }
    }

}