using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace House_Rent.Models
{
    public class HouseFeatures
    {
        public int HouseFeaturesID { get; set; }

        public string FeatureValue { get; set; }

        [Column("FeatureID")]
        public int? FeatureID { get; set; }
        public virtual Feature Feature { get; set; }




        [Column("HouseID")]
        public int? HouseID { get; set; }
        public virtual House House { get; set; }


        public bool Status { get; set; }//Deleted or Not
        public Nullable<DateTime> CreatedOn { get; set; }//Creation Date
        public string CreatedBy { get; set; }//the user created this record
        public Nullable<DateTime> ModifyOn { get; set; }//Modfiy Date
        public string ModifyBy { get; set; }//the Last user Modified this record
    }
}