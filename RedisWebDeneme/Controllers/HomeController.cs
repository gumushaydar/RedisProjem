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
    public class HomeController : Controller
    {
        RedisCache redisManager = new RedisCache();

     
        public ActionResult Index()
        {
            return View();

        }
    


    }
}