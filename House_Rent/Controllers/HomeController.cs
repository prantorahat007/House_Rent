using House_Rent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static House_Rent.Models.ViewModel;

namespace House_Rent.Controllers
{ 
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            return View(db.Houses.Where(x=>x.Status == true).ToList());
        }

        public ActionResult SearchPartial()
        {
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName");
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.HouseTypeID = new SelectList(db.HouseTypes, "HouseTypeID", "HouseTypeName");
            ViewBag.RoomCount = new SelectList(db.HouseFeatures.Where(x => x.FeatureID == 1).Select(x => x.FeatureValue).Distinct().ToList());
            ViewBag.BadeCount = new SelectList(db.HouseFeatures.Where(x => x.FeatureID == 2).Select(x => x.FeatureValue).Distinct().ToList());
            ViewBag.BathRoomCount = new SelectList(db.HouseFeatures.Where(x => x.FeatureID == 3).Select(x => x.FeatureValue).Distinct().ToList());
            ViewBag.HousePrice = new SelectList(db.Houses.Where(x => x.Status == true).Select(x => x.HousePrice * 30).Distinct().ToList());
            return PartialView("_HouseSearch");
        }



        [HttpGet]
        public ActionResult Search()
        {
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName");
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.HouseTypeID = new SelectList(db.HouseTypes, "HouseTypeID", "HouseTypeName");
            ViewBag.RoomCount = new SelectList(db.HouseFeatures.Where(x => x.FeatureID == 1).Select(x => x.FeatureValue).Distinct().ToList());
            ViewBag.BadeCount = new SelectList(db.HouseFeatures.Where(x => x.FeatureID == 2).Select(x => x.FeatureValue).Distinct().ToList());
            ViewBag.BathRoomCount = new SelectList(db.HouseFeatures.Where(x => x.FeatureID == 3).Select(x => x.FeatureValue).Distinct().ToList());
            ViewBag.HousePrice = new SelectList(db.Houses.Where(x => x.Status == true).Select(x => x.HousePrice * 30).Distinct().ToList());
            return View("Search", db.Houses.ToList());
        }

        [HttpPost]
        public ActionResult Search(HouseSearchVM searchVM)
        {
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName");
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.HouseTypeID = new SelectList(db.HouseTypes, "HouseTypeID", "HouseTypeName");
            ViewBag.RoomCount = new SelectList(db.HouseFeatures.Where(x => x.FeatureID == 1).Select(x => x.FeatureValue).Distinct().ToList());
            ViewBag.BadeCount = new SelectList(db.HouseFeatures.Where(x => x.FeatureID == 2).Select(x => x.FeatureValue).Distinct().ToList());
            ViewBag.BathRoomCount = new SelectList(db.HouseFeatures.Where(x => x.FeatureID == 3).Select(x => x.FeatureValue).Distinct().ToList());
            ViewBag.HousePrice = new SelectList(db.Houses.Where(x => x.Status == true).Select(x => x.HousePrice).Distinct().ToList());

            List<House> houses = db.Houses.Where(x =>
       (string.IsNullOrEmpty(searchVM.SearchKey) || (x.HouseAddress.Contains(searchVM.SearchKey) || x.Host.CustomerName.Contains(searchVM.SearchKey) || x.HouseName.Contains(searchVM.SearchKey))) &&
      (string.IsNullOrEmpty(searchVM.HouseTypeID.ToString()) || x.HouseTypeID == searchVM.HouseTypeID) &&
      (string.IsNullOrEmpty(searchVM.CityID.ToString()) || x.CityID == searchVM.CityID) &&
      (string.IsNullOrEmpty(searchVM.HousePrice.ToString()) || (x.HousePrice * 30) >= searchVM.HousePrice) &&
      (string.IsNullOrEmpty(searchVM.RoomCount.ToString()) || x.HouseFeatures.Any(y => y.FeatureID == 1 && y.FeatureValue == searchVM.RoomCount.ToString())) &&
      (string.IsNullOrEmpty(searchVM.BadeCount.ToString()) || x.HouseFeatures.Any(y => y.FeatureID == 2 && y.FeatureValue == searchVM.BadeCount.ToString())) &&
      (string.IsNullOrEmpty(searchVM.BathRoomCount.ToString()) || x.HouseFeatures.Any(y => y.FeatureID == 3 && y.FeatureValue == searchVM.BathRoomCount.ToString()))
      ).ToList();

            return View("Search", houses);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application beautifull page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            House house = db.Houses.Find(id);
            if (house == null)
            {
                return HttpNotFound();
            }
            return View(house);
        }
    }
}