using System.ComponentModel.DataAnnotations;

namespace PersonalInfoSampleApp.Model
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string CityName { get; set; }

        [Required]
        [StringLength(40)]
        public string Powiat { get; set; }

        [Required]
        [StringLength(40)]
        public string Voivodeship { get; set; }
    }
}