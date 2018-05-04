using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using MySqlProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(MySqlProject.Startup))]
namespace MySqlProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            using (ApplicationDbContext context = new ApplicationDbContext())
            {

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                // In Startup iam creating first Admin Role and creating a default Admin User    
                if (!roleManager.RoleExists("Administrator"))
                {

                    // first we create Admin rool   
                    var role = new IdentityRole();
                    role.Name = "Administrator";
                    roleManager.Create(role);

                    //Here we create a Admin super user who will maintain the website   
                }


                var user = new ApplicationUser();
                // user.UserName = "Aisha";
                user.UserName = user.Email = "muhd@gmail.com";
                string userPWD = "Pa$$wo1d";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Administrator");
                }

                // creating Creating Manager role    
                if (!roleManager.RoleExists("Manager"))
                {
                    var role = new IdentityRole();
                    role.Name = "Manager";
                    roleManager.Create(role);

                }

                // creating Creating Employee role    
                if (!roleManager.RoleExists("Employee"))
                {
                    var role = new IdentityRole();
                    role.Name = "Employee";
                    roleManager.Create(role);

                }

            }
        }
    }
}
