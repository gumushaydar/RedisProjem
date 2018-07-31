using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Data;
using RedisWebDeneme.Models;

namespace RedisWebDeneme.ViewModel
{
    public class CacheObjectViewModel 
    {
        public AppConfig AppConfig { get; set; }
        public bool UseDbForPrices { get; set; }
        public User User { get; set; }
        public List<Products> Products { get; set; }
        
        public Channel Channel { get; set; }
        public KeyValue KeyValue { get; set; }

    }
}