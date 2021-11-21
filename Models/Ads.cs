using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AuthStore.Models
{
    [Table("Ads", Schema = "dbo")]
    public partial class Ads
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Ads ID")]
        public int AdsId { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Please Choose Front image.")]
        [Display(Name = "Front Image")]
        [NotMapped]
        public IFormFile FrontImage { get; set; }

    }
}
