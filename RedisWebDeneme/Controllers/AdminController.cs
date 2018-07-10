using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Newtonsoft.Json;
using RedisWebDeneme.Models;
using System.Xml;
using System.Windows.Forms;

namespace RedisWebDeneme.Controllers
{
    public class AdminController : Controller
    {
        
      
        RedisCache redisManager = new RedisCache();
        // GET: Admin

        
        public ActionResult Index()
        {

       
            return View();
        }


        public ActionResult addKey(string key, string value)
            {
            if (!redisManager.Exists(key))
            {
                
                CacheObject cacheObj = JsonConvert.DeserializeObject<CacheObject>(value);

                redisManager.Set<CacheObject>(key, cacheObj, DateTime.Now.AddMinutes(30));

            }

            return RedirectToAction("Index", "Home");
        }


    }
}