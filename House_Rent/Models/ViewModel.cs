using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace House_Rent.Models
{
    public class ViewModel
    {
        public class AdminUser
        {
            public string UserId { get; set; }

            [Required]
            public string Username { get; set; }
            public string Role { get; set; }
            public string UserImage { get; set; }
            public bool Status { get; set; }//Deleted or Not

            public Nullable<DateTime> CreatedOn { get; set; }//Creation Date
            public string CreatedBy { get; set; }//the user created this record
            public Nullable<DateTime> ModifyOn { get; set; }//Modfiy Date
            public string ModifyBy { get; set; }//the Last user Modified this record
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "كلمة المرور")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string RoleId { get; set; }
        }

        public class HouseVM
        {
            [Display(Name = "Title")]
            [Required]
            public string HouseName { get; set; }
            public int? HouseID { get; set; }

            [Display(Name = "Description")]
            [MinLength(100)]
            public string HouseDescription { get; set; }

            [Display(Name = "Address")]
            [Required]
            public string HouseAddress { get; set; }

            [Display(Name = "Type")]
            [Required]
            public int? HouseTypeID { get; set; }

            [Display(Name = "City")]
            [Required]
            public int? CityID { get; set; }

            [Display(Name = "Country")]
            [Required]
            public int? CountryID { get; set; }

            [Display(Name = "State")]
            [Required]
            public int? StateID { get; set; }

            [Display(Name = "Price Per Day")]
            [Required]
            public double HousePrice { get; set; }

            [Display(Name = "Image")]
            [Required]
            public HttpPostedFileBase House_Image { get; set; }

            public List<HouseFeatures> HouseFeatures { get; set; }

            public string HouseImage { get; set; }


        }
        public class HouseSearchVM
        {
            [Display(Name = "Keyword")]
            public string SearchKey { get; set; }

            [Display(Name = "Type")]
            public int? HouseTypeID { get; set; }

            [Display(Name = "City")]
            public int? CityID { get; set; }
            [Display(Name = "Rooms")]
            public int? RoomCount { get; set; }
            [Display(Name = "Beds")]
            public int? BadeCount { get; set; }
            [Display(Name = "Bathrooms")]
            public int? BathRoomCount { get; set; }

            [Display(Name = "Country")]
            public int? CountryID { get; set; }

            [Display(Name = "State")]
            public int? StateID { get; set; }

            [Display(Name = "Price")]
            public double? HousePrice { get; set; }
        }
        public class BeOwnerVM
        {
            [Display(Name ="Passport")]
            public string CustomerPassportID { get; set; }
            public HttpPostedFileBase CustomerPassportImage { get; set; }

        }
        public class RequestBookingVM
        {
            public int HouseID { get; set; }
            public string CheckIn { get; set; }
            public string CheckOut { get; set; }
            public string Comment { get; set; }
        }
    }

}