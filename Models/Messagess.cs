using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthStore.Models
{
    public class Messagess
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Text { get; set; }
        public DateTime When { get; set; }

        public string UserID { get; set; }
        public virtual AppUser Sender { get; set; }

        //[StringLength(500)]
        //public string ImageUrl { get; set; }

        //[Required(ErrorMessage = "Please Choose Front image.")]
        //[Display(Name = "Front Image")]
        //[NotMapped]
        //public IFormFile FrontImage { get; set; }
        public Messagess()
        {
            When = DateTime.Now;
        }
    } 
}
