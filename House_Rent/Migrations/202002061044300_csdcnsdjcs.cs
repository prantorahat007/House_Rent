namespace House_Rent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class csdcnsdjcs : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bookings", "BookingHostComment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "BookingHostComment", c => c.String());
        }
    }
}
