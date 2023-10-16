using System;
using System.Collections.Generic;

namespace PropertyRental.Models
{
    public partial class User
    {
        public User()
        {
            AppointmentPropertyManagers = new HashSet<Appointment>();
            AppointmentTenants = new HashSet<Appointment>();
            EventPropertyManagers = new HashSet<Event>();
            EventPropertyOwners = new HashSet<Event>();
            Logins = new HashSet<Login>();
            MessageReceivers = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
        }

        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int RoleId { get; set; }
        public string? Phone { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Appointment> AppointmentPropertyManagers { get; set; }
        public virtual ICollection<Appointment> AppointmentTenants { get; set; }
        public virtual ICollection<Event> EventPropertyManagers { get; set; }
        public virtual ICollection<Event> EventPropertyOwners { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Message> MessageReceivers { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
    }
}
