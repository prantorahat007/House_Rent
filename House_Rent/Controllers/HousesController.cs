using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using House_Rent.Models;
using static House_Rent.Models.ViewModel;

namespace House_Rent.Controllers
{
    public class HousesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string GetUserID()
        {
            ApplicationUser user = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            return user.Id;
        }

        // GET: Houses
        public ActionResult Index()
        {
            String UserID = GetUserID();
            List<House> houses = db.Houses.Include(h => h.City).Include(h => h.Host).Include(h => h.HouseType).Where(x => x.HostID == UserID && x.Status==true).ToList();

            return View(houses);
        }

        // GET: Houses/Details/5
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

        // GET: Houses/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName");
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.HouseTypeID = new SelectList(db.HouseTypes, "HouseTypeID", "HouseTypeName");
            ViewBag.Features = db.Features.ToList();
            return View();
        }

        // POST: Houses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HouseVM houseVM, int? FeatureID1, int? FeatureID2, int? FeatureID3)
        {
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName");
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.HouseTypeID = new SelectList(db.HouseTypes, "HouseTypeID", "HouseTypeName");
            ViewBag.Features = db.Features.ToList();

            House house = new House();
            house.HouseName = houseVM.HouseName;
            house.HousePrice = houseVM.HousePrice;
            house.CityID = houseVM.CityID;
            house.HouseTypeID = houseVM.HouseTypeID;
            house.HostID = GetUserID();
            house.HouseAddress = houseVM.HouseAddress;
            house.HouseDescription = houseVM.HouseDescription;
            house.CreatedBy = User.Identity.Name;
            house.CreatedOn = DateTime.Now;
            house.Status = true;

            if (houseVM.House_Image != null)
            {

                string HouseImage = UploadPostedFile(houseVM.House_Image, "~/Content/Uploads/Images/Houses/");
                if (!string.IsNullOrWhiteSpace(HouseImage))
                {

                    house.HouseImage = HouseImage;
                    db.Houses.Add(house);
                    db.SaveChanges();

                    AddFeture(1, house.HouseID, FeatureID1.ToString());
                    AddFeture(2, house.HouseID, FeatureID2.ToString());
                    AddFeture(3, house.HouseID, FeatureID3.ToString());

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error Save HouseImage");
                }

            }
            else
            {
                ModelState.AddModelError("", "Error Save HouseImage");
            }
            return View(houseVM);
        }
        private void AddFeture(int featureID, int houseID, string featureValue)
        {
            HouseFeatures houseFeatures = new HouseFeatures();
            houseFeatures.HouseID = houseID;
            houseFeatures.FeatureValue = featureValue;
            houseFeatures.FeatureID = featureID;
            db.HouseFeatures.Add(houseFeatures);
            db.SaveChanges();
        }
        private void EditHouseFeture(int featureID, int houseID, string featureValue)
        {
            HouseFeatures houseFeatures = db.HouseFeatures.Where(x => x.FeatureID == featureID && x.HouseID == houseID).FirstOrDefault();
            houseFeatures.FeatureValue = featureValue;
            db.Entry(houseFeatures).State = EntityState.Modified;
            db.SaveChanges();
        }
        private string UploadPostedFile(HttpPostedFileBase file, string path)
        {

            string ext = file.ContentType.ToLower();
            ext = ext.Split('/')[1].ToString();

            string fname = Helpers.Helper.RandomString(16) + "." + ext;
            var fileName = fname;
            try
            {
                if (file.FileName.Length > 0)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(Server.MapPath(path));
                    }
                    fname = Path.Combine(Server.MapPath(path), fname);
                    if (!System.IO.File.Exists(fname))
                    {
                        file.SaveAs(fname);
                    }
                    else
                    {
                        UploadPostedFile(file, path);
                    }
                }
                return fileName;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        // GET: Houses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseVM house = db.Houses.Where(x => x.HouseID == id).Select(x => new HouseVM()
            {
                HouseID = x.HouseID,
                CityID = x.CityID,
                CountryID = x.City.State.CountryID,
                HouseAddress = x.HouseAddress,
                HouseDescription = x.HouseDescription,
                HouseFeatures = x.HouseFeatures.ToList(),
                HouseImage = x.HouseImage,
                HouseName = x.HouseName,
                HousePrice = x.HousePrice,
                HouseTypeID = x.HouseTypeID,
                StateID = x.City.StateID
            }).FirstOrDefault();
            if (house == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName", house.CountryID);
            ViewBag.StateID = new SelectList(db.States, "StateID", "StateName", house.StateID);
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName", house.CityID);
            ViewBag.HouseTypeID = new SelectList(db.HouseTypes, "HouseTypeID", "HouseTypeName", house.HouseTypeID);

            return View(house);
        }

        // POST: Houses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(House houseVM, int id, int FeatureID1, int FeatureID2, int FeatureID3)
        {
            try
            {
                House house = db.Houses.Where(x => x.HouseID == id).FirstOrDefault();

                house.HouseName = houseVM.HouseName;
                house.HousePrice = houseVM.HousePrice;
                house.CityID = houseVM.CityID;
                house.HouseTypeID = houseVM.HouseTypeID;
                house.HostID = GetUserID();
                house.HouseAddress = houseVM.HouseAddress;
                house.HouseDescription = houseVM.HouseDescription;
                house.CreatedBy = User.Identity.Name;
                house.CreatedOn = DateTime.Now;
                house.Status = true;

                db.Entry(house).State = EntityState.Modified;
                db.SaveChanges();

                EditHouseFeture(1, house.HouseID, FeatureID1.ToString());
                EditHouseFeture(2, house.HouseID, FeatureID2.ToString());
                EditHouseFeture(3, house.HouseID, FeatureID3.ToString());

                return RedirectToAction("Index");
            }
            catch
            {
                return View(houseVM);
            }
        }

        // GET: Houses/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Houses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            House house = db.Houses.Find(id);
            house.Status = false;
            db.Entry(house).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
