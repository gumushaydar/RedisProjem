namespace RedisWebDeneme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KeyValues", "Channels_ChannelId", "dbo.Channels");
            DropIndex("dbo.KeyValues", new[] { "Channels_ChannelId" });
            RenameColumn(table: "dbo.KeyValues", name: "Channels_ChannelId", newName: "ChannelId");
            AlterColumn("dbo.KeyValues", "ChannelId", c => c.Int(nullable: false));
            CreateIndex("dbo.KeyValues", "ChannelId");
            AddForeignKey("dbo.KeyValues", "ChannelId", "dbo.Channels", "ChannelId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KeyValues", "ChannelId", "dbo.Channels");
            DropIndex("dbo.KeyValues", new[] { "ChannelId" });
            AlterColumn("dbo.KeyValues", "ChannelId", c => c.Int());
            RenameColumn(table: "dbo.KeyValues", name: "ChannelId", newName: "Channels_ChannelId");
            CreateIndex("dbo.KeyValues", "Channels_ChannelId");
            AddForeignKey("dbo.KeyValues", "Channels_ChannelId", "dbo.Channels", "ChannelId");
        }
    }
}
