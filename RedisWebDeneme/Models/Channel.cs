using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedisWebDeneme.Models
{
    public class Channel
    {
        public int ChannelId { get; set; }
        public string  ChannelName { get; set; }

        public virtual List<KeyValue> KeyValues { get; set; }
    }
}