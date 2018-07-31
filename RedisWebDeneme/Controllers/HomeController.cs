using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Newtonsoft.Json;
using System.Xml;
using System.Windows.Forms;
using StackExchange.Redis;
using RedisWebDeneme.ViewModel;
using RedisWebDeneme.Models;
using RedisWebDeneme.Data;

namespace RedisWebDeneme.Controllers
{
    public class HomeController : Controller
    {
       
        RedisCache redisManager = new RedisCache();

        public ActionResult Index()
        {


            var keyListArray = redisManager.GetAllKeys();


            string[] valueAndKey = new string[keyListArray.Length];

            for (int i = 0; i < keyListArray.Length; i++)
            {

               valueAndKey[i] = keyListArray[i] + " ==> " + JsonConvert.SerializeObject(redisManager.Get(keyListArray[i]));

            }

            ViewData["ValueAndKey"] = valueAndKey;


            return View();

        }


    }
}