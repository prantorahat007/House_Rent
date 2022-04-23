namespace House_Rent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingID = c.Int(nullable: false, identity: true),
                        BookingCheckIn = c.DateTime(),
                        BookingCheckOut = c.DateTime(),
                        BookingTotalPrice = c.Double(nullable: false),
                        BookingSiteFee = c.Double(nullable: false),
                        BookingDate = c.DateTime(),
                        CancelDate = c.DateTime(),
                        BookingCancelReason = c.String(),
                        BookingRefund = c.Double(nullable: false),
                        IsConfirm = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifyOn = c.DateTime(),
                        ModifyBy = c.String(),
                        HouseID = c.Int(),
                        Guest_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BookingID)
                .ForeignKey("dbo.AspNetUsers", t => t.Guest_Id)
                .ForeignKey("dbo.Houses", t => t.HouseID)
                .Index(t => t.HouseID)
                .Index(t => t.Guest_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserImage = c.String(),
                        CustomerName = c.String(),
                        CustomerDescription = c.String(),
                        CustomerTypeID = c.Int(),
                        Status = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifyOn = c.DateTime(),
                        ModifyBy = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerTypes", t => t.CustomerTypeID)
                .Index(t => t.CustomerTypeID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Houses",
                c => new
                    {
                        HouseID = c.Int(nullable: false, identity: true),
                        HouseName = c.String(),
                        HouseDescription = c.String(),
                        HouseAddress = c.String(),
                        HouseImage = c.String(),
                        HouseLat = c.Double(nullable: false),
                        HouseLong = c.Double(nullable: false),
                        HouseStatus = c.Boolean(nullable: false),
                        HousePrice = c.Double(nullable: false),
                        HostID = c.String(maxLength: 128),
                        HouseTypeID = c.Int(),
                        CityID = c.Int(),
                        Status = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifyOn = c.DateTime(),
                        ModifyBy = c.String(),
                    })
                .PrimaryKey(t => t.HouseID)
                .ForeignKey("dbo.Cities", t => t.CityID)
                .ForeignKey("dbo.AspNetUsers", t => t.HostID)
                .ForeignKey("dbo.HouseTypes", t => t.HouseTypeID)
                .Index(t => t.HostID)
                .Index(t => t.HouseTypeID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        StateID = c.Int(),
                        Status = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifyOn = c.DateTime(),
                        ModifyBy = c.String(),
                    })
                .PrimaryKey(t => t.CityID)
                .ForeignKey("dbo.States", t => t.StateID)
                .Index(t => t.StateID);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateID = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                        CountryID = c.Int(),
                        Status = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifyOn = c.DateTime(),
                        ModifyBy = c.String(),
                    })
                .PrimaryKey(t => t.StateID)
                .ForeignKey("dbo.Countries", t => t.CountryID)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifyOn = c.DateTime(),
                        ModifyBy = c.String(),
                    })
                .PrimaryKey(t => t.CountryID);
            
            CreateTable(
                "dbo.HouseFeatures",
                c => new
                    {
                        HouseFeaturesID = c.Int(nullable: false, identity: true),
                        FeatureValue = c.String(),
                        FeatureID = c.Int(),
                        HouseID = c.Int(),
                        Status = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifyOn = c.DateTime(),
                        ModifyBy = c.String(),
                    })
                .PrimaryKey(t => t.HouseFeaturesID)
                .ForeignKey("dbo.Features", t => t.FeatureID)
                .ForeignKey("dbo.Houses", t => t.HouseID)
                .Index(t => t.FeatureID)
                .Index(t => t.HouseID);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        FeatureID = c.Int(nullable: false, identity: true),
                        FeatureName = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifyOn = c.DateTime(),
                        ModifyBy = c.String(),
                    })
                .PrimaryKey(t => t.FeatureID);
            
            CreateTable(
                "dbo.HouseTypes",
                c => new
                    {
                        HouseTypeID = c.Int(nullable: false, identity: true),
                        HouseTypeName = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifyOn = c.DateTime(),
                        ModifyBy = c.String(),
                    })
                .PrimaryKey(t => t.HouseTypeID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        BookingID = c.Int(),
                        SenderID = c.String(maxLength: 128),
                        ResiverID = c.String(maxLength: 128),
                        Amount = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        Tax = c.Double(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifyOn = c.DateTime(),
                        ModifyBy = c.String(),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.Bookings", t => t.BookingID)
                .ForeignKey("dbo.AspNetUsers", t => t.ResiverID)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderID)
                .Index(t => t.BookingID)
                .Index(t => t.SenderID)
                .Index(t => t.ResiverID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "SenderID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "ResiverID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "BookingID", "dbo.Bookings");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Bookings", "HouseID", "dbo.Houses");
            DropForeignKey("dbo.Houses", "HouseTypeID", "dbo.HouseTypes");
            DropForeignKey("dbo.HouseFeatures", "HouseID", "dbo.Houses");
            DropForeignKey("dbo.HouseFeatures", "FeatureID", "dbo.Features");
            DropForeignKey("dbo.Houses", "HostID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Houses", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Cities", "StateID", "dbo.States");
            DropForeignKey("dbo.States", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CustomerTypeID", "dbo.CustomerTypes");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "Guest_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Transactions", new[] { "ResiverID" });
            DropIndex("dbo.Transactions", new[] { "SenderID" });
            DropIndex("dbo.Transactions", new[] { "BookingID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.HouseFeatures", new[] { "HouseID" });
            DropIndex("dbo.HouseFeatures", new[] { "FeatureID" });
            DropIndex("dbo.States", new[] { "CountryID" });
            DropIndex("dbo.Cities", new[] { "StateID" });
            DropIndex("dbo.Houses", new[] { "CityID" });
            DropIndex("dbo.Houses", new[] { "HouseTypeID" });
            DropIndex("dbo.Houses", new[] { "HostID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "CustomerTypeID" });
            DropIndex("dbo.Bookings", new[] { "Guest_Id" });
            DropIndex("dbo.Bookings", new[] { "HouseID" });
            DropTable("dbo.Transactions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.HouseTypes");
            DropTable("dbo.Features");
            DropTable("dbo.HouseFeatures");
            DropTable("dbo.Countries");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Houses");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.CustomerTypes");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Bookings");
        }
    }
}
