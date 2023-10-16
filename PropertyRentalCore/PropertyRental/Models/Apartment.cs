using System;
using System.Collections.Generic;

namespace PropertyRental.Models
{
    public partial class Apartment
    {
        public Apartment()
        {
            ApartmentImages = new HashSet<ApartmentImage>();
            Events = new HashSet<Event>();
        }

        public int ApartmentId { get; set; }
        public int PropertyManagerId { get; set; }
        public int AddressId { get; set; }
        public int StatusId { get; set; }
        public int BuildingId { get; set; }
        public int? NumberOfRooms { get; set; }
        public string? Amenities { get; set; }
        public decimal? Price { get; set; }
        public int? Floor { get; set; }
        public int? ConstructionYear { get; set; }
        public double? Area { get; set; }

        public virtual ICollection<ApartmentImage> ApartmentImages { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
