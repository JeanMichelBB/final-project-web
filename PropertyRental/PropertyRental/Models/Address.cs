using System;
using System.Collections.Generic;

namespace PropertyRental.Models
{
    public partial class Address
    {
        public Address()
        {
            Appointments = new HashSet<Appointment>();
            Users = new HashSet<User>();
        }

        public int AddressId { get; set; }
        public string? StreetName { get; set; }
        public string? StreetNumber { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Province { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
