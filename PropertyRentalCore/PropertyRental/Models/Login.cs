using System;
using System.Collections.Generic;

namespace PropertyRental.Models
{
    public partial class Login
    {
        public int LoginId { get; set; }
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
