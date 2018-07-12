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

            //if (!context.Products.Any())
            //{
            //    // ALTRI PRODOTTI
            //    var altri_prodotti = context.Categories.FirstOrDefault(q => q.Name.Equals("Altri prodotti"));

            //    context.Products.AddRange(
            //        new List<Product>(){
            //            new Product {
            //                ProductID = Guid.NewGuid(),
            //                Category = altri_prodotti,
            //                CategoryID = altri_prodotti.CategoryID,
            //                Code = "papaya",
            //                MarcucciCode = "papaya_fermentata",
            //                Name = "Papaya Fermentata",
            //                Price = 10,
            //                Available = true,
            //                IsDeleted = false,
            //                Quantity = 10,
            //                ImageUrl = "/Content/img/Products/papaya_fermentata.jpg",
            //                ShortDescription = "Papaya Fermentata",
            //                LongDescription = "Papaya Fermentata 60cps blist",
            //                Note = "Assumere con un bicchiere d'acqua"
            //            },
            //            new Product {
            //                ProductID = Guid.NewGuid(),
            //                Category = altri_prodotti,
            //                CategoryID = altri_prodotti.CategoryID,
            //                Code = "echinaid",
            //                MarcucciCode = "echinaid_altapotenza",
            //                Name = "Echinaid Alta Potenza",
            //                Price = 10,
            //                Available = true,
            //                IsDeleted = false,
            //                Quantity = 10,
            //                ImageUrl = "/Content/img/Products/echinaid.jpg",
            //                ShortDescription = "Echinaid Alta Potenza",
            //                LongDescription = "Echinaid Alta Potenza",
            //                Note = "Assumere con un bicchiere d'acqua"
            //            },
            //            new Product {
            //                ProductID = Guid.NewGuid(),
            //                Category = altri_prodotti,
            //                CategoryID = altri_prodotti.CategoryID,
            //                Code = "echinaid_urto",
            //                MarcucciCode = "echinaid_urto",
            //                Name = "Echinaid Urto",
            //                Price = 10,
            //                Available = true,
            //                IsDeleted = false,
            //                Quantity = 10,
            //                ImageUrl = "/Content/img/Products/echinaid_urto.jpg",
            //                ShortDescription = "Echinaid Urto",
            //                LongDescription = "Echinaid Urto 30cps",
            //                Note = "Assumere con un bicchiere d'acqua"
            //            },
            //            new Product {
            //                ProductID = Guid.NewGuid(),
            //                Category = altri_prodotti,
            //                CategoryID = altri_prodotti.CategoryID,
            //                Code = "immunilflor",
            //                MarcucciCode = "immunilflor",
            //                Name = "Immunilflor",
            //                Price = 10,
            //                Available = true,
            //                IsDeleted = false,
            //                Quantity = 10,
            //                ImageUrl = "/Content/img/Products/immunilflor.jpg",
            //                ShortDescription = "Immunilflor",
            //                LongDescription = "Immunilflor 30cps",
            //                Note = "Assumere con un bicchiere d'acqua"
            //            },
            //            new Product {
            //                ProductID = Guid.NewGuid(),
            //                Category = altri_prodotti,
            //                CategoryID = altri_prodotti.CategoryID,
            //                Code = "propolaid_propolgola",
            //                MarcucciCode = "propolaid_propolgola",
            //                Name = "Propolaid Propolgola",
            //                Price = 10,
            //                Available = true,
            //                IsDeleted = false,
            //                Quantity = 10,
            //                ImageUrl = "/Content/img/Products/propolaid.jpg",
            //                ShortDescription = "Propolaid Propolgola",
            //                LongDescription = "Propolaid Propolgola 30cps",
            //                Note = "Succhiare in bocca fino a scioglimento"
            //            },
            //            new Product {
            //                ProductID = Guid.NewGuid(),
            //                Category = altri_prodotti,
            //                CategoryID = altri_prodotti.CategoryID,
            //                Code = "gse_biotic_forte",
            //                MarcucciCode = "gse_biotic_forte",
            //                Name = "GSE Biotic Forte",
            //                Price = 10,
            //                Available = true,
            //                IsDeleted = false,
            //                Quantity = 10,
            //                ImageUrl = "/Content/img/Products/biotic_forte.jpg",
            //                ShortDescription = "GSE Biotic Forte",
            //                LongDescription = "GSE Biotic Forte 2blist x 12cpr",
            //                Note = "Assumere con un bicchiere d'acqua"
            //            },
            //        }
            //    );
            //}

            //context.SaveChanges();
        }
    }
}
