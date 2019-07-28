using System.ComponentModel.DataAnnotations;

namespace PersonalInfoSampleApp.Pages.Form.ViewModel
{
    public class AddressViewModel
    {
        [StringLength(50, ErrorMessage = "LengthViolation")]
        [Required(ErrorMessage = "FieldRequired")]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [StringLength(20, ErrorMessage = "LengthViolation")]
        [Required(ErrorMessage = "FieldRequired")]
        [Display(Name = "ResidenceNumber")]
        public string ResidenceNumber { get; set; }

        [StringLength(6, ErrorMessage = "LengthViolation")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage ="PostalCodeFormat")]
        [Required(ErrorMessage = "FieldRequired")]
        [Display(Name = "PostalNumber")]
        public string PostalNumber { get; set; }

        [Required(ErrorMessage = "FieldRequired")]
        [Display(Name = "City")]
        public int CityId { get; set; }
    }
}