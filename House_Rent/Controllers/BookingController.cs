using House_Rent.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static House_Rent.Models.ViewModel;

namespace House_Rent.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public string GetUserID()
        {
            ApplicationUser user = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            return user.Id;
        }

        [HttpGet]
        public ActionResult MyBookings()
        {
            var userID = GetUserID();
            List<Booking> mybookings = db.Bookings.Where(x => x.GuestID == userID && x.Status).ToList();
            return View(mybookings);
        }

        [HttpPost]
        public ActionResult UpdateBookingComment(int id, string bookingComment)
        {
            Booking booking = db.Bookings.Where(x => x.BookingID == id).FirstOrDefault();

            booking.BookingComment = bookingComment;
            db.Entry(booking).State = EntityState.Modified;
            db.SaveChanges();

            return Json(bookingComment);
        }


        [HttpGet]
        public ActionResult BookingRequests()
        {
            var userID = GetUserID();
            List<Booking> mybookings = db.Bookings.Where(x => x.House.HostID == userID && x.Status).ToList();
            return View(mybookings);
        }

        [HttpGet]
        public ActionResult ChangeBookingStatus(int id, int BookingStatusID)
        {
            Booking booking = db.Bookings.Where(x => x.BookingID == id && x.Status == true).FirstOrDefault();
            booking.BookingStatusID = BookingStatusID;
            db.Entry(booking).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("BookingRequests");
        }

        [HttpPost]
        public ActionResult RequestBooking(RequestBookingVM bookingVM)
        {
            try
            {
                Booking booking = new Booking()
                {
                    HouseID = bookingVM.HouseID,
                    GuestID = GetUserID(),
                    BookingCheckIn = DateTime.Parse(bookingVM.CheckIn),
                    BookingCheckOut = DateTime.Parse(bookingVM.CheckOut),
                    BookingComment = bookingVM.Comment,
                    Status = true,
                    CreatedOn = DateTime.Now,
                    CreatedBy = User.Identity.Name,
                    BookingStatusID = 1,
                };
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("MyBookings");
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
        }
    }
}