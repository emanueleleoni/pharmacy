using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public class ShippingDetail
    {
        [Key]
        public Guid ShippingID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
