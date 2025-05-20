using System.ComponentModel.DataAnnotations;

namespace AdvancedAjax.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [Range(0, 9999.99)]
        public decimal Price { get; set; }
    }
}