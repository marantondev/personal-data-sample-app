using System.ComponentModel.DataAnnotations;

namespace PersonalInfoSampleApp.Pages.Form.ViewModel
{
    public class AddressViewModel
    {
        [StringLength(50)]
        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "ResidenceNumber")]
        public string ResidenceNumber { get; set; }

        [StringLength(6)]
        [RegularExpression(@"^\d{2}-\d{3}$")]
        [Required]
        [Display(Name = "PostalNumber")]
        public string PostalNumber { get; set; }

        [Required]
        [Display(Name = "City")]
        public int CityId { get; set; }
    }
}