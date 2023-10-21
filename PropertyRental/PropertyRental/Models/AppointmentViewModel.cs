using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyRental.Models
{
    public class AppointmentViewModel : Appointment
    {
        [Display(Name = "Hour")]
        public string SelectedTime { get; set; }
        [Display(Name = "Date")]
        public string SelectedDate { get; set; }

/*        public DateTime Timestamp
        {
            get
            {
                return SelectedDate.Date(SelectedHour).AddMinutes(SelectedMinute);
            }
        }*/
    }
}