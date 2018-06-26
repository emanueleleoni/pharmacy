namespace DAL.Migrations
{
    using DAL.Model;
    using DataAccess;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DAL.Context.ApplicationDbContext";
        }

        protected override void Seed(DAL.Context.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (context.Roles.All(x => x.Name != RuoliUtente.admin))
                roleManager.Create(new IdentityRole() { Name = RuoliUtente.admin });

            if (context.Roles.All(x => x.Name != RuoliUtente.consumer))
                roleManager.Create(new IdentityRole() { Name = RuoliUtente.consumer });

            if (context.Users.All(x => x.UserName != "administrator"))
            {
                var user = new ApplicationUser() { UserName = "administrator@pagita.com", Email = "administrator@pagita.com", EmailConfirmed = true };
                userManager.Create(user, "Password1");
                userManager.AddToRole(user.Id, RuoliUtente.admin);
            }

            if (context.Users.All(x => x.UserName != "consumer1"))
            {
                var user = new ApplicationUser() { UserName = "consumer1@pagita.com", Email = "consumer1@pagita.com", EmailConfirmed = true };
                userManager.Create(user, "Password1");
                userManager.AddToRole(user.Id, RuoliUtente.consumer);
            }

            if (context.Users.All(x => x.UserName != "consumer2"))
            {
                var user = new ApplicationUser() { UserName = "consumer2@pagita.com", Email = "consumer2@pagita.com", EmailConfirmed = true };
                userManager.Create(user, "Password1");
                userManager.AddToRole(user.Id, RuoliUtente.consumer);
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                new List<Category>() {
                    new Category {
                        CategoryID = Guid.NewGuid(),
                        Description = "Farmaci",
                        Name = "Farmaci"
                    },
                    new Category {
                        CategoryID = Guid.NewGuid(),
                        Description = "Altri prodotti",
                        Name = "Altri prodotti"
                    }
                });
            }

            if (!context.Products.Any())
            {

            }
        }
    }
}
