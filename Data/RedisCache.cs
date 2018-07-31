using System;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using Data;

using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Data
{
    public class RedisCache : ICache
    {

        
        private readonly IDatabase _redisDb;
        private  ISubscriber pubsub;

        public RedisCache()
        {
           
            _redisDb = RedisConnectionFactory.Connection.GetDatabase();
            pubsub = RedisConnectionFactory.Connection.GetSubscriber();
            
        }
        public T Get<T>(string key)
        {
            string redisObject = _redisDb.StringGet(key);

            return SerializeHelper.Deserialize<T>(redisObject);

        }

        public void Set<T>(string key, T objectToCache, DateTime expireDate)
        {
            var expireTimeSpan = expireDate.Subtract(DateTime.Now);

            _redisDb.StringSet(key, SerializeHelper.Serialize(objectToCache), expireTimeSpan);

        }
        public void SetString(string key,string obj, DateTime expireDate)
        {
            var expireTimeSpan = expireDate.Subtract(DateTime.Now);

            _redisDb.StringSet(key,obj, expireTimeSpan);

        }

        public string Get(string key)
        {
            string redisObject = _redisDb.StringGet(key);

            return redisObject;

        }

        public string[] GetAllKeys()
        {
            var myList = new List<string>();
            IServer server = RedisConnectionFactory.Connection.GetServer("localhost", 6379);
            foreach (var key in server.Keys())
            {
                if(key != "ChannelsKey")
                    myList.Add(key);
            }
            string[] myArr = myList.ToArray();
            return myArr;
        }

        public void Delete(string key)
        {
            _redisDb.KeyDelete(key);
        }

        public bool Exists(string key)
        {
            return _redisDb.KeyExists(key);
        }

        public void Dispose()
        {

            RedisConnectionFactory.Connection.Dispose();

        }


        public void Publish(string channel, string message)
        {

            pubsub.Publish(channel, message);

        }

        public  void Subscribe(string channelName)
        {
            
            pubsub.Subscribe(channelName, (channel, message) => MessageAction(message));

        }

        public  void UnSubscribe(string channelName)
        {
            pubsub.Unsubscribe(channelName);
        }

        public  void MessageAction(RedisValue message)
        {
           

            Console.WriteLine(message);
            

        }

        //public void Set(string key, string data, int cacheTime)
        //{
        //    if (data == null)
        //        return;

        //    _redisDb.SetAdd(key, data);

        //    var expiresIn = TimeSpan.FromMinutes(cacheTime);

        //    if (cacheTime > 0)
        //        _redisDb.KeyExpire(key, expiresIn);
        //}




    }

}
