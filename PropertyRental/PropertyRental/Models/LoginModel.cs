using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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

        private int roleID = 4;


        public int LoginID { get => loginID; set => loginID = value; }
        public int UserID { get => userID; set => userID = value; }
        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid Email Address"), MaxLength(50) ]
        public string Email { get => email; set => email = value; }
        [Required(ErrorMessage = "Password is required"), MaxLength(50)]
        public string Password { get => password; set => password = value; }
        [Required(ErrorMessage = "First Name is required"), MaxLength(50)]
        public string FirstName { get => firstName; set => firstName = value; }
        [Required(ErrorMessage = "Last Name is required"), MaxLength(50)]
        public string LastName { get => lastName; set => lastName = value; }
        [Required(ErrorMessage = "Phone is required"), MaxLength(10)]
        public string Phone { get => phone; set => phone = value; }
        [Required(ErrorMessage = "Street Name is required"), MaxLength(50)]
        public string StreetName { get => streetName; set => streetName = value; }
        [Required(ErrorMessage = "Street Number is required"), MaxLength(50)]
        public string StreetNumber { get => streetNumber; set => streetNumber = value; }
        [Required(ErrorMessage = "City is required"), MaxLength(50)]
        public string City { get => city; set => city = value; }
        [Required(ErrorMessage = "Postal Code is required"), MaxLength(6)]
        public string PostalCode { get => postalCode; set => postalCode = value; }
        [Required(ErrorMessage = "Country is required"), MaxLength(50)]
        public string Country { get => country; set => country = value; }
        [Required(ErrorMessage = "Province is required"), MaxLength(50)]
        public string Province { get => province; set => province = value; }
        public int RoleID { get => roleID; set => roleID = value; }
    }
}