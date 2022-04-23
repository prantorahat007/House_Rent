﻿using House_Rent.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

namespace House_Rent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Host"))
            {
                // first we create Admin role   
                var role = new IdentityRole()
                {
                    Name = "Host"
                };
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Guest"))
            {
                // first we create Admin role   
                var role = new IdentityRole()
                {
                    Name = "Guest"
                };
                roleManager.Create(role);
            }

            // In Startup I am creating first Admin Role and creating a default Admin User   
            if (!roleManager.RoleExists("Administrator"))
            {

                // first we create Admin role   
                var role = new IdentityRole()
                {
                    Name = "Administrator"
                };
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                 

                var user = new ApplicationUser()
                {
                    UserName = "Admin",
                    Email = "Admin123@gmail.com",
                    //CreatedBy = "Admin",
                    //CreatedOn = DateTime.Now
                };
                string userPWD = "Admin@QziUs8";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin  
                if (chkUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Administrator");
                }
            }



        }

    }
}
