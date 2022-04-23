using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace House_Rent.Models
{
    public class Feature
    {
        public int FeatureID { get; set; }
        public string FeatureName { get; set; }

        public bool Status { get; set; }//Deleted or Not
        public Nullable<DateTime> CreatedOn { get; set; }//Creation Date
        public string CreatedBy { get; set; }//the user created this record
        public Nullable<DateTime> ModifyOn { get; set; }//Modfiy Date
        public string ModifyBy { get; set; }//the Last user Modified this record
    }
}