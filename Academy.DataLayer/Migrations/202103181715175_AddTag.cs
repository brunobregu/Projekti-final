namespace Academy.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Tag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Tag");
        }
    }
}
