using System;
using System.Collections.Generic;

namespace PropertyRental.Models
{
    public partial class Building
    {
        public int BuildingId { get; set; }
        public int AddressId { get; set; }
        public int? NumberOfFloors { get; set; }
        public int? ConstructionYear { get; set; }
        public string? Amenities { get; set; }
    }
}
