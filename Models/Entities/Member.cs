using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Models
{
    public class Member
    {
        public int Id { get; set; }

        [Required, StringLength(30, ErrorMessage = "Do not enter more than 30 characters"), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, StringLength(30, ErrorMessage = "Do not enter more than 30 characters"), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Do not enter more than 60 characters"), Display(Name = "E-mail")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [Required, StringLength(30, ErrorMessage = "Do not enter more than 30 characters"), Display(Name = "Phone number")]
        public string PhoneNum { get; set; }
        [Display(Name = "Full name")]
        public string FullName => $"{FirstName} {LastName}";

        public ICollection<ParkedVehicle> ParkedVehicles { get; set; }

        //[Required]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]

        //public string Password { get; set; }

        //[Required]
        //[System.ComponentModel.DataAnnotations.Compare("Password")]
        //public string ConfirmPassword { get; set; }

    }
}
