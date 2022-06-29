namespace NaptaWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ons : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserPlants", newName: "PlantApplicationUsers");
            DropPrimaryKey("dbo.PlantApplicationUsers");
            AddColumn("dbo.Comment", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Comment", "Content", c => c.String(nullable: false));
            AddPrimaryKey("dbo.PlantApplicationUsers", new[] { "Plant_ID", "ApplicationUser_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PlantApplicationUsers");
            AlterColumn("dbo.Comment", "Content", c => c.String());
            DropColumn("dbo.Comment", "Email");
            AddPrimaryKey("dbo.PlantApplicationUsers", new[] { "ApplicationUser_Id", "Plant_ID" });
            RenameTable(name: "dbo.PlantApplicationUsers", newName: "ApplicationUserPlants");
        }
    }
}
