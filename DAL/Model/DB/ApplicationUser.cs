using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataAccess
{
    public enum ApplicationUserType
    {
        Consumer, Administrator
    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class,
    // please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string VAT { get; set; }

        public string City { get; set; }

        public ApplicationUserType UserType { get; set; }

        public bool IsProfileCompleted { get; set; }

        public string PictureURL { get; set; }

        public string CF { get; set; }

        public string RfId { get; set; }

        public bool IsDeleted { get; set; }

        public string Agreement { get; set; }

        public DateTime Birthdate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}