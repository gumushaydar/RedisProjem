namespace RedisWebDeneme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Channels",
                c => new
                    {
                        ChannelId = c.Int(nullable: false, identity: true),
                        ChannelName = c.String(),
                    })
                .PrimaryKey(t => t.ChannelId);
            
            CreateTable(
                "dbo.KeyValues",
                c => new
                    {
                        KeyValueId = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        ChannelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KeyValueId)
                .ForeignKey("dbo.Channels", t => t.ChannelId, cascadeDelete: true)
                .Index(t => t.ChannelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KeyValues", "ChannelId", "dbo.Channels");
            DropIndex("dbo.KeyValues", new[] { "ChannelId" });
            DropTable("dbo.KeyValues");
            DropTable("dbo.Channels");
        }
    }
}
