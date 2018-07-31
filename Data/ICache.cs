using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface ICache : IDisposable
    {
        T Get<T>(string key);

        void Set<T>(string key, T obj, DateTime expireDate);
        void SetString(string key, string obj, DateTime expireDate);
        void Delete(string key);

        bool Exists(string key);

        void Subscribe(string channelName);

        void UnSubscribe(string channelName);

        void Publish(string channelName, string message);
        string[] GetAllKeys();
        string Get(string key);




    }
}
