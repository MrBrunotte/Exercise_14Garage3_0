using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class MemberOverViewViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Full name")]
        public string FullName => $"{FirstName} {LastName}";

        //[Display(Name = "Owner")]
        //public Member MemberPr { get; set; }

        [Display(Name = "Owner")]
        public string Member { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNum { get; set; }
       
        [Display(Name = "Number of vehicles")]
        public int NumOfVehicles { get; set; }
    }
}
