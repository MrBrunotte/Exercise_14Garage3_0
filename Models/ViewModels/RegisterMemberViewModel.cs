using System.ComponentModel.DataAnnotations;
namespace Garage3.Models.ViewModels
{
    public class RegisterMemberViewModel
    {
        public int MemberID { get; set; }

        [Required, StringLength(30, ErrorMessage = "Do not enter more than 30 characters"), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, StringLength(30, ErrorMessage = "Do not enter more than 30 characters"), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Do not enter more than 60 characters"), Display(Name = "E-mail")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        //[Remote(action: "CheckEmail", controller: "ParkedVehicle")]
        //[EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(30, ErrorMessage = "Do not enter more than 30 characters"), Display(Name = "Phone number")]
        public string PhoneNum { get; set; }
        [Display(Name = "Full name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}
