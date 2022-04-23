namespace House_Rent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kldsf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CustomerPassportID", c => c.String());
            AddColumn("dbo.AspNetUsers", "CustomerPassportImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CustomerPassportImage");
            DropColumn("dbo.AspNetUsers", "CustomerPassportID");
        }
    }
}
