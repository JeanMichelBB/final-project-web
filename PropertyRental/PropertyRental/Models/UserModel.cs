using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyRental.Models
{
    public class UserModel
    {
        private int loginID;
        private int userID;
        private string email;
        private string password;

        private string firstName;
        private string lastName;
        private string phone;

        private int streetName;
        private int streetNumber;
        private int city;
        private int postalCode;
        private int country;
        private int province;
        private int cityName;

        private int roleID = 4;


        public int LoginID { get => loginID; set => loginID = value; }
        public int UserID { get => userID; set => userID = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Phone { get => phone; set => phone = value; }

        public int StreetName { get => streetName; set => streetName = value; }
        public int StreetNumber { get => streetNumber; set => streetNumber = value; }
        public int City { get => city; set => city = value; }
        public int PostalCode { get => postalCode; set => postalCode = value; }
        public int Country { get => country; set => country = value; }
        public int Province { get => province; set => province = value; }
        public int CityName { get => cityName; set => cityName = value; }

        public int RoleID { get => roleID; set => roleID = value; }


    }
}