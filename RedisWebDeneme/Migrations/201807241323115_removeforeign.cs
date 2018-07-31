namespace RedisWebDeneme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeforeign : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KeyValues", "ChannelId", "dbo.Channels");
            DropIndex("dbo.KeyValues", new[] { "ChannelId" });
            RenameColumn(table: "dbo.KeyValues", name: "ChannelId", newName: "Channels_ChannelId");
            AlterColumn("dbo.KeyValues", "Channels_ChannelId", c => c.Int());
            CreateIndex("dbo.KeyValues", "Channels_ChannelId");
            AddForeignKey("dbo.KeyValues", "Channels_ChannelId", "dbo.Channels", "ChannelId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KeyValues", "Channels_ChannelId", "dbo.Channels");
            DropIndex("dbo.KeyValues", new[] { "Channels_ChannelId" });
            AlterColumn("dbo.KeyValues", "Channels_ChannelId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.KeyValues", name: "Channels_ChannelId", newName: "ChannelId");
            CreateIndex("dbo.KeyValues", "ChannelId");
            AddForeignKey("dbo.KeyValues", "ChannelId", "dbo.Channels", "ChannelId", cascadeDelete: true);
        }
    }
}
