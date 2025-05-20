using System.ComponentModel.DataAnnotations;

namespace AdvancedAjax.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(75)]
        public string Name { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; } = "";
    }
}
