using System;
using System.Collections.Generic;

namespace PropertyRental.Models
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public int PropertyManagerId { get; set; }
        public int TenantId { get; set; }
        public DateTime? Timestamp { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual User PropertyManager { get; set; } = null!;
        public virtual User Tenant { get; set; } = null!;
    }
}
