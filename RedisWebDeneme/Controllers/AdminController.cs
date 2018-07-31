using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Newtonsoft.Json;
using RedisWebDeneme.ViewModel;
using StackExchange.Redis;

namespace RedisWebDeneme.Controllers
{
    public class AdminController : Controller
    {
   
        
        RedisCache redisManager = new RedisCache();

        public ActionResult Index()
        {

             
            
            return View();
        }


        public ActionResult addKey(string key, string value)
            {

            redisManager.Subscribe("channel1");

            if (!redisManager.Exists(key))
            {
                redisManager.Set<CacheObject>(key, SerializeHelper.Deserialize<CacheObject>(value), DateTime.Now.AddMinutes(20));
            }
            else
            {
                redisManager.Delete(key);

                redisManager.Set<CacheObject>(key, SerializeHelper.Deserialize<CacheObject>(value), DateTime.Now.AddMinutes(20));
            }

            return RedirectToAction("Index", "Home");
        }


    }
}