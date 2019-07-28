using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalInfoSampleApp.Pages.Form.ViewModel
{
    [Display(Name = "PersonalInfo")]
    public class PersonalInfoViewModel
    {
        [StringLength(50, ErrorMessage = "LengthViolation")]
        [Required(ErrorMessage = "FieldRequired")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "LengthViolation")]
        [Required(ErrorMessage = "FieldRequired")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "DateOfBirth")]
        [Required(ErrorMessage = "FieldRequired")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "UseSameAddress")]
        public bool UseSameAddress { get; set; }

        [Required(ErrorMessage = "FieldRequired")]
        [Display(Name = "ResidenceAddress")]
        public AddressViewModel ResidenceAddress { get; set; }

        [Display(Name = "CorrespondenceAddress")]
        public AddressViewModel CorrespondenceAddress { get; set; }
    }
}
