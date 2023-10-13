using System;
using System.Collections.Generic;

namespace PropertyRental.Models
{
    public partial class EventType
    {
        public EventType()
        {
            Events = new HashSet<Event>();
        }

        public int EventTypeId { get; set; }
        public string? EventTypeName { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
