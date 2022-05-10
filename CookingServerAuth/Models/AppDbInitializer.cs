using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CookingServerAuth.Models
{
    public class AppDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var r_administrator = new IdentityRole { Name = "Administrator" };

            var userAdmin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" };

            var b_administrator = roleManager.Create(r_administrator).Succeeded;

            var b_Admin = userManager.Create(userAdmin, "Useradmin12345").Succeeded;

            if (b_administrator && b_Admin)
            {
                userManager.AddToRole(userAdmin.Id, r_administrator.Name);
            }

            base.Seed(context);
        }
    }
}