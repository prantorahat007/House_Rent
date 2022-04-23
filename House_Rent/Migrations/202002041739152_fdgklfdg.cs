namespace House_Rent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdgklfdg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "CustomerTypeID", "dbo.CustomerTypes");
            DropIndex("dbo.AspNetUsers", new[] { "CustomerTypeID" });
            DropColumn("dbo.AspNetUsers", "CustomerTypeID");
            DropTable("dbo.CustomerTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CustomerTypes",
                c => new
                    {
                        CustomerTypeID = c.Int(nullable: false, identity: true),
                        CustomerTypeName = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifyOn = c.DateTime(),
                        ModifyBy = c.String(),
                    })
                .PrimaryKey(t => t.CustomerTypeID);
            
            AddColumn("dbo.AspNetUsers", "CustomerTypeID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "CustomerTypeID");
            AddForeignKey("dbo.AspNetUsers", "CustomerTypeID", "dbo.CustomerTypes", "CustomerTypeID");
        }
    }
}
