using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DeletedApplicationUser
    {
        public string Id { get; set; }

        public string Username { get; set; }

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
    }
}