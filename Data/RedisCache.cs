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
        private ISubscriber pubsub;

        public RedisCache()
        {
            _redisDb = RedisConnectionFactory.Connection.GetDatabase();
            pubsub = RedisConnectionFactory.Connection.GetSubscriber();
        }

        public void Set<T>(string key, T objectToCache, DateTime expireDate)
        {
            var expireTimeSpan = expireDate.Subtract(DateTime.Now);

            _redisDb.StringSet(key, JsonConvert.SerializeObject(objectToCache), expireTimeSpan);

        }

        public T Get<T>(string key)
        {
            var redisObject = _redisDb.StringGet(key);

            return redisObject.HasValue ? JsonConvert.DeserializeObject<T>(redisObject) : Activator.CreateInstance<T>();
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

        public void Subscribe(string channelName)
        {
            pubsub.Subscribe(channelName, (channel, message) => MessageAction(message));
        }

        public void UnSubscribe(string channelName)
        {
            pubsub.Unsubscribe(channelName);
        }

        public void MessageAction(RedisValue message)
        {
            Console.WriteLine(message);
        }
    }
}
