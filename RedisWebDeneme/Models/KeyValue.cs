using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedisWebDeneme.Models
{
    public class KeyValue
    {
        public int KeyValueId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public virtual Channel Channels { get; set; }
    }
}