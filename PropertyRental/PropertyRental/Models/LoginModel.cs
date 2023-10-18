using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyRental.Models
{
    public class LoginModel
    {
        private int loginID;
        private int userID;
        private string email;
        private string password;

        private string firstName;
        private string lastName;
        private string phone;

        private string streetName;
        private string streetNumber;
        private string city;
        private string postalCode;
        private string country;
        private string province;
        private string cityName;

        private int roleID = 4;


        public int LoginID { get => loginID; set => loginID = value; }
        public int UserID { get => userID; set => userID = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Phone { get => phone; set => phone = value; }

        public string StreetName { get => streetName; set => streetName = value; }
        public string StreetNumber { get => streetNumber; set => streetNumber = value; }
        public string City { get => city; set => city = value; }
        public string PostalCode { get => postalCode; set => postalCode = value; }
        public string Country { get => country; set => country = value; }
        public string Province { get => province; set => province = value; }
        public string CityName { get => cityName; set => cityName = value; }

        public int RoleID { get => roleID; set => roleID = value; }


    }
}