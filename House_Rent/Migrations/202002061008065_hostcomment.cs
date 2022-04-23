namespace House_Rent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hostcomment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "BookingHostComment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "BookingHostComment");
        }
    }
}
