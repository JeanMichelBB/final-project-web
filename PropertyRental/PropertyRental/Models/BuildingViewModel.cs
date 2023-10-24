using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyRental.Models
{
    public class BuildingViewModel : Building
    {
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
    }
}