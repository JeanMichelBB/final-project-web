using System;
using System.Collections.Generic;

namespace PropertyRental.Models
{
    public partial class ApartmentImage
    {
        public int ImageId { get; set; }
        public int ApartmentId { get; set; }
        public string? ImageUrl { get; set; }

        public virtual Apartment Apartment { get; set; } = null!;
    }
}
