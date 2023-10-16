using System;
using System.Collections.Generic;

namespace PropertyRental.Models
{
    public partial class Event
    {
        public int EventId { get; set; }
        public int? PropertyManagerId { get; set; }
        public int? PropertyOwnerId { get; set; }
        public string? EventDescription { get; set; }
        public int? ApartmentId { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? EventTypeId { get; set; }

        public virtual Apartment? Apartment { get; set; }
        public virtual EventType? EventType { get; set; }
        public virtual User? PropertyManager { get; set; }
        public virtual User? PropertyOwner { get; set; }
    }
}
