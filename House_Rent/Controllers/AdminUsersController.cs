using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using House_Rent.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using static House_Rent.Models.ViewModel;

namespace House_Rent.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminUsersController : Controller
    {
        private ApplicationDbContext db;
        public AdminUsersController()
        {
            if (db != null)
            {
                if (db.Database.Connection.State == ConnectionState.Closed)
                {
                    db.Database.Connection.Open();
                }

            }
            else
            {
                db = new ApplicationDbContext();
            }
        }

        // GET: Admin/AdminUsers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(String searchName)
        {
            if (!Request.IsAjaxRequest())
            {
                return PartialView("Error");
            }
            ViewBag.roleList = new SelectList(db.Roles.ToList(), "Name", "Name");
            List<AdminUser> userList = (from user in db.Users
                                        select new
                                        {
                                            UserId = user.Id,
                                            Username = user.UserName,
                                            user.Email,
                                            UserImage = user.UserImage,
                                            user.CreatedOn,
                                            user.CreatedBy,
                                            user.ModifyBy,
                                            user.ModifyOn,
                                            RoleNames = (from userRole in user.Roles
                                                         join role in db.Roles on userRole.RoleId
                                                         equals role.Id
                                                         select role.Name).ToList()
                                        }).ToList().Select(p => new AdminUser()

                                        {
                                            UserId = p.UserId,
                                            Username = p.Username,
                                            Email = p.Email,
                                            UserImage = p.UserImage,
                                            ModifyOn = p.ModifyOn,
                                            ModifyBy = p.ModifyBy,
                                            CreatedOn = p.CreatedOn,
                                            CreatedBy = p.CreatedBy,
                                            Role = string.Join(",", p.RoleNames)
                                        }).ToList();

            foreach (AdminUser userVM in userList)
            {

                String UserImage = "/Content/Uploads/Images/Users/avatar.png";
                if (userVM.UserImage != null)
                {
                    if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/Uploads/Images/Users/"), userVM.UserId + "." + userVM.UserImage)))
                    {
                        UserImage = "/Content/Uploads/Images/Users/" + userVM.UserId + "." + userVM.UserImage + "?" + new Random().Next(10000); ;
                    }
                }
                userVM.UserImage = UserImage;
            }

            return PartialView("_Search", userList);
        }
        public ActionResult Create()
        {
            ViewBag.roleList = new SelectList(db.Roles.Where(x => x.Name != "User").ToList().OrderBy(x => x.Id), "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdminUser userVM, HttpPostedFileBase image)
        {

  
            try
            {

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                var user = new ApplicationUser();
                user.UserName = userVM.Username.Replace(" ", "_");
                user.Email = userVM.Email;
                user.CreatedBy = User.Identity.GetUserId();
                user.CreatedOn = DateTime.Now;
                string userPWD = userVM.Password;

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin  
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, userVM.RoleId);
                    if (image != null)
                    {
                        if (image.FileName.Length > 0)
                        {
                            string fname;

                            // Checking for Internet Explorer  
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = image.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = image.FileName;
                            }
                            string ext = image.ContentType.ToLower();
                            ext = ext.Split('/')[1].ToString();
                            user.UserImage = ext;
                            db.Entry(user);
                            UserManager.Update(user);
                            db.SaveChanges();
                            fname = user.Id + "." + ext;
                            fname = Path.Combine(Server.MapPath("~/Content/Uploads/Images/Users"), fname);
                            image.SaveAs(fname);
                        }
                    }
                }
                else
                {
                    return Json(new { result = false, responseText = "خطاء اسم المستخدم موجود بالفعل !" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { result = true, responseText = "تم الاضافة بنجاح" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Error " + ex.Message.ToString();
                ViewBag.roleList = new SelectList(db.Roles.ToList(), "Name", "Name");
                return Json(new { result = false, responseText = "خطاء " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(String Id)
        {
            if (!Request.IsAjaxRequest())
            {
                return PartialView("Error");
            }
            AdminUser userVM = (from user in db.Users
                                select new
                                {
                                    UserId = user.Id,
                                    Username = user.UserName,
                                    Email = user.Email,
                                    UserImage = user.UserImage,
                                    CreatedOn = user.CreatedOn,
                                    CreatedBy = user.CreatedBy,
                                    ModifyBy = user.ModifyBy,
                                    ModifyOn = user.ModifyOn,
                                    RoleNames = (from userRole in user.Roles
                                                 join role in db.Roles on userRole.RoleId
                                                 equals role.Id
                                                 select role.Name).ToList()
                                }).ToList().Select(p => new AdminUser()

                                {
                                    UserId = p.UserId,
                                    Username = p.Username,
                                    Email = p.Email,
                                    UserImage = p.UserImage,
                                    ModifyOn = p.ModifyOn,
                                    ModifyBy = p.ModifyBy,
                                    CreatedOn = p.CreatedOn,
                                    CreatedBy = p.CreatedBy,
                                    Role = string.Join(",", p.RoleNames)
                                }).ToList().Where(x => x.UserId == Id).FirstOrDefault();
            String UserImage = "/Content/Uploads/Images/Users/avatar.png";
            if (userVM.UserImage != null)
            {
                if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/Uploads/Images/Users/"), userVM.UserId + "." + userVM.UserImage)))
                {
                    UserImage = "/Content/Uploads/Images/Users/" + userVM.UserId + "." + userVM.UserImage + "?" + new Random().Next(10000);
                }
            }
            userVM.UserImage = UserImage;

            ViewBag.roleList = new SelectList(db.Roles.Where(x => x.Name != "User").ToList().OrderBy(x => x.Id), "Name", "Name", userVM.Role);

            return PartialView("_Edit", userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminUser userVM, HttpPostedFileBase image, String Id)
        {

            if (!Request.IsAjaxRequest())
            {
                return PartialView("Error");
            }
            try
            {

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                var user = db.Users.Where(x => x.Id == Id).FirstOrDefault();
                user.UserName = userVM.Username.Replace(" ", "_");
                user.Email = userVM.Email;
                user.ModifyBy = User.Identity.GetUserId();
                user.ModifyOn = DateTime.Now;

                var chkUser = UserManager.Update(user);

                //Add default User to Role Admin  
                if (chkUser.Succeeded)
                {

                    var oldUser = UserManager.FindById(user.Id);
                    var oldRoleId = oldUser.Roles.SingleOrDefault().RoleId;
                    var oldRoleName = db.Roles.SingleOrDefault(r => r.Id == oldRoleId).Name;

                    if (oldRoleName != userVM.RoleId)
                    {
                        UserManager.RemoveFromRole(user.Id, oldRoleName);
                        UserManager.AddToRole(user.Id, userVM.RoleId);
                    }
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();

                    if (image != null)
                    {
                        if (image.FileName.Length > 0)
                        {
                            string fname;

                            // Checking for Internet Explorer  
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = image.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = image.FileName;
                            }
                            string ext = image.ContentType.ToLower();
                            ext = ext.Split('/')[1].ToString();
                            user.UserImage = ext;
                            db.Entry(user);
                            UserManager.Update(user);
                            db.SaveChanges();
                            fname = user.Id + "." + ext;
                            fname = Path.Combine(Server.MapPath("~/Content/Uploads/Images/Users"), fname);
                            image.SaveAs(fname);
                        }
                    }
                }
                else
                {
                    return Json(new { result = false, responseText = "اسم المستخدم موجود بالفعل !" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { result = true, responseText = "تم التعديل بنجاح" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.roleList = new SelectList(db.Roles.ToList(), "Name", "Name");
                return Json(new { result = false, responseText = "خطاء " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                ApplicationUser user = db.Users.Where(x => x.Id == id).FirstOrDefault();
                UserManager.Delete(user);
                db.SaveChanges();
                return Json(new { result = true, responseText = "تم الحذف بنجاح" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, responseText = "خطاء " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}