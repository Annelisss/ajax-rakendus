﻿using Microsoft.AspNetCore.Http;

namespace AdvancedAjax.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(75)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(75)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-Mail is not Valid")]
        public string EmailId { get; set; }


        [Required]
        [DisplayName("Country")]
        [NotMapped]
        public int CountryId { get; set; }


        [Required]
        [ForeignKey("City")]
        [DisplayName("City")]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        [Required(ErrorMessage = "Please choose the photo for the customer")]
        [MaxLength(500)]
        public string PhotoUrl { get; set; }


        [Display(Name = "Profile Photo")]

        [NotMapped]
        public string BreifPhotoName { get; set; }

        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }
    }
}
