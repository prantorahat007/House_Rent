using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace House_Rent.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }

        [Column("BookingID")]
        public int? BookingID { get; set; }
        public virtual Booking Booking { get; set; }
        
        [Column("SenderID")]
        public string SenderID { get; set; }
        public virtual ApplicationUser Sender { get; set; }


        [Column("ResiverID")]
        public string ResiverID { get; set; }
        public virtual ApplicationUser Resiver { get; set; }

        public double Amount { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        
        public bool Status { get; set; }//Deleted or Not
        public Nullable<DateTime> CreatedOn { get; set; }//Creation Date
        public string CreatedBy { get; set; }//the user created this record
        public Nullable<DateTime> ModifyOn { get; set; }//Modfiy Date
        public string ModifyBy { get; set; }//the Last user Modified this record
    }
}