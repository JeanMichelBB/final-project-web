//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PropertyRental.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Apartment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Apartment()
        {
            this.ApartmentImages = new HashSet<ApartmentImage>();
            this.Events = new HashSet<Event>();
        }
    
        public int ApartmentID { get; set; }
        public int PropertyManagerID { get; set; }
        public int AddressID { get; set; }
        public int StatusID { get; set; }
        public int BuildingID { get; set; }
        public Nullable<int> NumberOfRooms { get; set; }
        public string Amenities { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Floor { get; set; }
        public Nullable<int> ConstructionYear { get; set; }
        public Nullable<double> Area { get; set; }
    
        public virtual Address Address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApartmentImage> ApartmentImages { get; set; }
        public virtual Building Building { get; set; }
        public virtual User User { get; set; }
        public virtual Status Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }
    }
}
