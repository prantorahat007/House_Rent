using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace House_Rent.Models
{
    public class City
    {
        [Key]
        public int CityID { get; set; }
        [Display(Name ="City")]
        public string CityName { get; set; }

        [Column("StateID")]
        public int? StateID { get; set; }
        public virtual State State { get; set; }

        public bool Status { get; set; }//Deleted or Not
        public Nullable<DateTime> CreatedOn { get; set; }//Creation Date
        public string CreatedBy { get; set; }//the user created this record
        public Nullable<DateTime> ModifyOn { get; set; }//Modfiy Date
        public string ModifyBy { get; set; }//the Last user Modified this record
    }
}