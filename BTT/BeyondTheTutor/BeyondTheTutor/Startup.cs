using Microsoft.Owin;
using Owin;
using System.Collections.Generic;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;

[assembly: OwinStartupAttribute(typeof(BeyondTheTutor.Startup))]
namespace BeyondTheTutor
{
    public partial class Startup
    {
        List<string> ROLES = new List<string>() { "Admin", "Professor", "Student", "Tutor" };

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        // See: https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97
        // In this method we will create default User roles and Admin user for login   
        private void CreateRolesandUsers()
        {
            // The context that Identity created
            ApplicationDbContext context = new ApplicationDbContext();

            //the main database
            BeyondTheTutorContext db = new BeyondTheTutorContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            
            /*roleManager.Delete(roleManager.FindByName(ROLES[0]));
            roleManager.Delete(roleManager.FindByName(ROLES[1]));
            roleManager.Delete(roleManager.FindByName(ROLES[2]));
            roleManager.Delete(roleManager.FindByName(ROLES[3]));*/


            // Create admin role and seed with the admin user/
            // Assumes neither already exists
            if (!roleManager.RoleExists(ROLES[0]))
            {
                // Create role
                var role = new IdentityRole(ROLES[0]);       // role name is "Admin"
                IdentityResult res = roleManager.Create(role);

                // Create user with this role
                string userPWD = "admin2020";// System.Web.Configuration.WebConfigurationManager.AppSettings["AdminPassword"];
                string userEmail = "admin@beyondthetutor.com";// System.Web.Configuration.WebConfigurationManager.AppSettings["AdminEmail"];
                var user = new ApplicationUser { 
                    UserName = userEmail, 
                    Email = userEmail
                };
                // Username and email must be the same unless you want to make changes to the login code, which assumes they are the same
                // It will appear to work but once you clear your cache (to delete the cookie) or use another browser it won't work

                res = UserManager.Create(user, userPWD);

                if (res.Succeeded) { var result1 = UserManager.AddToRole(user.Id, ROLES[0]); }
            }

            // Do we need another role?  i.e. "User"

            // Create admin role and seed with the admin user/
            // Assumes neither already exists
            if (!roleManager.RoleExists(ROLES[1]))
            {
                // Create role
                var role = new IdentityRole(ROLES[1]);       // role name is "Professor"
                IdentityResult res = roleManager.Create(role);

                // Create user with this role
                string userPWD = "professor2020";// System.Web.Configuration.WebConfigurationManager.AppSettings["AdminPassword"];
                string userEmail = "professor@beyondthetutor.com";// System.Web.Configuration.WebConfigurationManager.AppSettings["AdminEmail"];
                var user = new ApplicationUser { UserName = userEmail, Email = userEmail };
                // Username and email must be the same unless you want to make changes to the login code, which assumes they are the same
                // It will appear to work but once you clear your cache (to delete the cookie) or use another browser it won't work

                res = UserManager.Create(user, userPWD);

                if (res.Succeeded)
                {
                    var special_user = new Professor
                    {
                        FirstName = "default",
                        LastName = "default",
                        ASPNetIdentityID = user.Id
                    };
                    db.Professors.Add(special_user);
                    db.SaveChangesAsync();
                }

                if (res.Succeeded) { var result1 = UserManager.AddToRole(user.Id, ROLES[1]); }
            }

            // Create admin role and seed with the admin user/
            // Assumes neither already exists
            if (!roleManager.RoleExists(ROLES[2]))
            {
                // Create role
                var role = new IdentityRole(ROLES[2]);       // role name is "Student"
                IdentityResult res = roleManager.Create(role);

                // Create user with this role
                string userPWD = "student2020";// System.Web.Configuration.WebConfigurationManager.AppSettings["AdminPassword"];
                string userEmail = "student@beyondthetutor.com";// System.Web.Configuration.WebConfigurationManager.AppSettings["AdminEmail"];
                var user = new ApplicationUser 
                { 
                    UserName = userEmail, 
                    Email = userEmail
                };
                // Username and email must be the same unless you want to make changes to the login code, which assumes they are the same
                // It will appear to work but once you clear your cache (to delete the cookie) or use another browser it won't work

                res = UserManager.Create(user, userPWD);

                if (res.Succeeded)
                {
                    var special_user = new Student
                    {
                        FirstName = "default",
                        LastName = "default",
                        ClassStanding = "default",
                        GraduatingYear = DateTime.Today,
                        ASPNetIdentityID = user.Id
                    };
                    db.Students.Add(special_user);
                    db.SaveChangesAsync();
                }

                if (res.Succeeded) { var result1 = UserManager.AddToRole(user.Id, ROLES[2]); }
            }

            // Create admin role and seed with the admin user/
            // Assumes neither already exists
            if (!roleManager.RoleExists(ROLES[3]))
            {
                // Create role
                var role = new IdentityRole(ROLES[3]);       // role name is "Tutor"
                IdentityResult res = roleManager.Create(role);

                // Create user with this role
                string userPWD = "tutor2020";// System.Web.Configuration.WebConfigurationManager.AppSettings["AdminPassword"];
                string userEmail = "tutor@beyondthetutor.com";// System.Web.Configuration.WebConfigurationManager.AppSettings["AdminEmail"];
                var user = new ApplicationUser { UserName = userEmail, Email = userEmail };
                // Username and email must be the same unless you want to make changes to the login code, which assumes they are the same
                // It will appear to work but once you clear your cache (to delete the cookie) or use another browser it won't work

                res = UserManager.Create(user, userPWD);

                if (res.Succeeded)
                {
                    var special_user = new Tutor
                    {
                        FirstName = "default",
                        LastName = "default",
                        VNumber = "V00000000",
                        ClassOf = DateTime.Today,
                        ASPNetIdentityID = user.Id
                    };
                    db.Tutors.Add(special_user);
                    db.SaveChangesAsync();
                }

                if (res.Succeeded) { var result1 = UserManager.AddToRole(user.Id, ROLES[3]); }
            }



            /*
            // creating Creating Professor role   
            if (!roleManager.RoleExists(ROLES[1])) // Professor Role
            {
                var role = new IdentityRole();
                role.Name = ROLES[1];
                roleManager.Create(role);
            }*/
        }
    }
}
