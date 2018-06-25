using DAL.Model;
using DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;

namespace DAL.Context
{
    [DbConfigurationType("DAL.Context.MyConfiguration, DAL")]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
#if DEBUG

        public ApplicationDbContext() : base("DevConnection", throwIfV1Schema: false)
        {
        }

#else
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
#endif

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Machine> Machines { get; set; }

        public DbSet<MachineMessage> MachineMessages { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<MachineError> MachineErrors { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Error> Errors { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<TransactionDetail> TransactionDetails { get; set; }

        public DbSet<MachineReservation> MachineReservations { get; set; }

        public DbSet<DeletedApplicationUser> DeletedUsers { get; set; }

        public DbSet<ShippingDetail> ShippingDetails { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // configures one-to-many relationship
            // Category può avere più prodotti e Product appartiene ad una categoria
            modelBuilder.Entity<Product>()
                .HasRequired<Category>(s => s.Category)
                .WithMany(g => g.Products)
                .HasForeignKey<Guid>(s => s.CategoryID);

            // Machine può avere più errori 
            modelBuilder.Entity<MachineError>()
                .HasRequired<Machine>(s => s.Machine)
                .WithMany(g => g.MachineErrors)
                .HasForeignKey<Guid>(s => s.MachineID);

            // Machine può avere più messaggi
            modelBuilder.Entity<MachineMessage>()
                .HasRequired<Machine>(s => s.Machine)
                .WithMany(g => g.MachineMessages)
                .HasForeignKey<Guid>(s => s.MachineID);

            // Machine può avere più prenotazioni
            modelBuilder.Entity<MachineReservation>()
                .HasRequired<Machine>(s => s.Machine)
                .WithMany(g => g.MachineReservations)
                .HasForeignKey<Guid>(s => s.MachineID);

            // Machine può avere più stocks
            modelBuilder.Entity<Stock>()
                .HasRequired<Machine>(s => s.Machine)
                .WithMany(g => g.Stocks)
                .HasForeignKey<Guid>(s => s.MachineID);

            // Product può avere più stocks
            modelBuilder.Entity<Stock>()
                .HasRequired<Product>(s => s.Product)
                .WithMany(g => g.Stocks)
                .HasForeignKey<Guid>(s => s.ProductID);

            // Product può essere associato a più transazioni
            modelBuilder.Entity<Transaction>()
                .HasRequired<Product>(s => s.Product)
                .WithMany(g => g.Transactions)
                .HasForeignKey<Guid>(s => s.ProductID);

            // ShippingDetail può essere associato a più transazioni
            modelBuilder.Entity<Transaction>()
                .HasRequired<ShippingDetail>(s => s.ShippingDetail)
                .WithMany(g => g.Transactions)
                .HasForeignKey<Guid>(s => s.ShippingID);

            // Machine può avere più transazioni
            modelBuilder.Entity<Transaction>()
                .HasOptional<Machine>(s => s.Machine)
                .WithMany(g => g.Transactions)
                .HasForeignKey<Guid?>(s => s.MachineID);

            // Product può essere associato a più TransactionDetail
            modelBuilder.Entity<TransactionDetail>()
                .HasRequired<Product>(s => s.Product)
                .WithMany(g => g.TransactionDetails)
                .HasForeignKey<Guid>(s => s.ProductID);

            // Configure one-to-one relationship
            // Transaction può avere un dettaglio
            modelBuilder.Entity<Transaction>()
                        .HasOptional(s => s.TransactionDetail) 
                        .WithRequired(ad => ad.Transaction);

            // Transaction può avere una MachineReservation
            modelBuilder.Entity<Transaction>()
                        .HasOptional(s => s.MachineReservation)
                        .WithRequired(ad => ad.Transaction);
        }
    }

    public class MyConfiguration : DbConfiguration
    {
        public MyConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new System.Data.Entity.SqlServer.SqlAzureExecutionStrategy(2, TimeSpan.FromSeconds(30)));
        }
    }

    public class ApplicationDbInitialize : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            BuildUsersAndRoles(context);

            context.SaveChanges();
        }

        private void BuildUsersAndRoles(ApplicationDbContext context)
        {
            var roleStore   = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore   = new UserStore<ApplicationUser>(context);
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
        }
    }
}
