using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace House_Rent.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }
        public Nullable<DateTime> BookingCheckIn { get; set; }
        public Nullable<DateTime> BookingCheckOut { get; set; }
        public string BookingComment { get; set; }
        public double BookingTotalPrice { get; set; }
        public double BookingSiteFee { get; set; }
        public Nullable<DateTime> BookingDate { get; set; }
        public Nullable<DateTime> CancelDate { get; set; }
        public string BookingCancelReason { get; set; }
        public double BookingRefund { get; set; }

        [Column("BookingStatusID")]
        public int? BookingStatusID { get; set; }
        public virtual BookingStatus BookingStatus { get; set; }

        public bool Status { get; set; }//Deleted or Not
        public Nullable<DateTime> CreatedOn { get; set; }//Creation Date
        public string CreatedBy { get; set; }//the user created this record
        public Nullable<DateTime> ModifyOn { get; set; }//Modfiy Date
        public string ModifyBy { get; set; }//the Last user Modified this record


        [Column("HouseID")]
        public int? HouseID { get; set; }
        public virtual House House { get; set; }

        [Column("GuestID")]
        public string GuestID { get; set; }
        public virtual ApplicationUser Guest { get; set; }

    }
}