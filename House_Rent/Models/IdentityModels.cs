using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace House_Rent.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string UserImage { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDescription { get; set; }
        public string CustomerPassportID { get; set; }
        public string CustomerPassportImage { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public bool Status { get; set; }//Deleted or Not
        public Nullable<DateTime> CreatedOn { get; set; }//Creation Date
        public string CreatedBy { get; set; }//the user created this record
        public Nullable<DateTime> ModifyOn { get; set; }//Modfiy Date
        public string ModifyBy { get; set; }//the Last user Modified this record

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<HouseType> HouseTypes { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<HouseFeatures> HouseFeatures { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingStatus> BookingStatuses { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

    }
}