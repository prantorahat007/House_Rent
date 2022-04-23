using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace House_Rent.Models
{
    public class State
    {
        [Key]
        public int StateID { get; set; }
        public string StateName { get; set; }

        [Column("CountryID")]
        public int? CountryID { get; set; }
        public virtual Country Country { get; set; }

        public bool Status { get; set; }//Deleted or Not
        public Nullable<DateTime> CreatedOn { get; set; }//Creation Date
        public string CreatedBy { get; set; }//the user created this record
        public Nullable<DateTime> ModifyOn { get; set; }//Modfiy Date
        public string ModifyBy { get; set; }//the Last user Modified this record
    }
}