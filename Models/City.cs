using System.ComponentModel.DataAnnotations;

namespace AdvancedAjax.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "City name is required")]
        [StringLength(100, ErrorMessage = "City name cannot be longer than 100 characters")]
        public string Name { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; } = "";
    }
}
