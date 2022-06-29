namespace NaptaWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ons1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plant", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Plant", "ImagePath");
        }
    }
}
