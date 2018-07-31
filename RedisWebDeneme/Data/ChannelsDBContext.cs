using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RedisWebDeneme.Models;

namespace RedisWebDeneme.Data
{
    public class ChannelsDBContext : DbContext
    {
        public DbSet<KeyValue> KeyValues { get; set; }

        public DbSet<Channel> Channels { get; set; }

    }
}