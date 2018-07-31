using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Newtonsoft.Json;
using RedisWebDeneme.Data;
using RedisWebDeneme.Models;
using RedisWebDeneme.ViewModel;

namespace RedisWebDeneme.Controllers
{
    public class ChannelController : Controller
    {   //DB obj
        ChannelsDBContext db = new ChannelsDBContext();

        // Cache obj
        RedisCache redisManager = new RedisCache();


        public ActionResult Index()
        {

            // DB den Cache atma
            var cachingOBJ = JsonConvert.SerializeObject(db.Channels.ToList(), Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            redisManager.SetString("ChannelsKey", cachingOBJ, DateTime.Now.AddMinutes(20));


            // Cache den Datalari cekme
            if (!redisManager.Exists("ChannelsKey"))
                return View();   
            List<Channel> ChannelsFromCache = JsonConvert.DeserializeObject <Channel[]>(redisManager.Get("ChannelsKey")).ToList();
            foreach (var channels in ChannelsFromCache)
            {
                redisManager.Subscribe(channels.ToString());
            }

            //List<Channel> ChannelList = ChannelsFromCache.ToList();

            return View(ChannelsFromCache);

        }

        public ActionResult AddChannel(string Channel)
        {
            Channel channelObj = new Channel
            {
                ChannelName = Channel
             
            };

            // null channel gondermemek icin kontrol
            if(Channel == "")
                return RedirectToAction("Index");

            // Ayni channel'i eklememek icin kontrol
            foreach (var item in db.Channels)
            {
                if (item.ChannelName == Channel)
                {
                    return RedirectToAction("Index");
                }
            }
            // Channellari DB ye kayit
            db.Channels.Add(channelObj);
            db.SaveChanges();
    

             return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Channels(int channelId)
        {
            
            
            var channel = db.Channels.FirstOrDefault(f => f.ChannelId == channelId);
            
            return View(channel);
        }




        [HttpPost]
        public ActionResult Channels(string channelName, int channelId, string Key, string Value)
        {
            redisManager.Publish(channelName,Key+"=>"+Value);
            var channel = db.Channels.FirstOrDefault(f => f.ChannelId == channelId);
            if (channel.KeyValues == null)
            {
                channel.KeyValues = new List<KeyValue>();
            }
            channel.KeyValues.Add(new KeyValue
            {
                Key = Key,

                Value = Value
            });
            
            db.SaveChanges();


            return RedirectToAction("Channels", new { channelId });
        }

        [HttpGet]
        public ActionResult Update(int? updateId)
        {
            
            var update = db.KeyValues.Where(x => x.KeyValueId == updateId).FirstOrDefault();
            
            return View(update);

        }

        [HttpPost]
        public ActionResult Update(KeyValue Obj,string channelName)
        {
            redisManager.Publish("channel1", Obj.Key+ "=>" + Obj.Value);
            db.Entry(Obj).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("Index");
        }
    
        
        public ActionResult Delete(int deleteId)
        {
            var delete = (from c in db.KeyValues
                          where c.KeyValueId == deleteId
                          select c).FirstOrDefault();
            db.Entry(delete).State = EntityState.Deleted;
            db.SaveChanges();

            return Redirect("Index");
        }

    }
}