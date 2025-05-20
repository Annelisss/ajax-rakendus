using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedAjax.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string EmailId { get; set; }

        [Required]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [Required]
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        public string Photo { get; set; } // failinimi
    }
}
