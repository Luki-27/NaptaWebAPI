namespace NaptaWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _as : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Disease", "Treatment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Disease", "Treatment");
        }
    }
}
