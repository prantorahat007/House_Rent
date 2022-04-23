using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace House_Rent.Models
{
    public class House
    {
        [Key]
        public int HouseID { get; set; }
        [DisplayName("House Name")]
        public string HouseName { get; set; }
        [DisplayName("House description")]
        public string HouseDescription { get; set; }
        [DisplayName("House address")]
        public string HouseAddress { get; set; }
        [DisplayName("House description")]
        public string HouseImage{ get; set; }
        public double HouseLat { get; set; }
        public double HouseLong { get; set; }
        public bool HouseStatus { get; set; }
        [DisplayName("Price")]
        public double HousePrice { get; set; }

        [Column("HostID")]
        public string HostID { get; set; }
        public virtual ApplicationUser Host { get; set; }
        
        [Column("HouseTypeID")]
        public int? HouseTypeID { get; set; }
        public virtual HouseType HouseType { get; set; }

        [Column("CityID")]
        public int? CityID { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<HouseFeatures> HouseFeatures { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public bool Status { get; set; }//Deleted or Not
        public Nullable<DateTime> CreatedOn { get; set; }//Creation Date
        public string CreatedBy { get; set; }//the user created this record
        public Nullable<DateTime> ModifyOn { get; set; }//Modfiy Date
        public string ModifyBy { get; set; }//the Last user Modified this record

    }
}