using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthStore.Models
{
    [Table("Message", Schema = "dbo")]
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Message")]
        public int MessageId { get; set; }

        [Required(ErrorMessage = "Please Enter Your Name.")]
        [Column(TypeName = "nvarchar(150)")]
        [MaxLength(145)]
        [Display(Name = "Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Your Phone.")]
        [Column(TypeName = "nvarchar(15)")]
        [MaxLength(11)]
        [Display(Name = "Your Phone")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Please Enter Your Mobaile.")]
        [Column(TypeName = "nvarchar(15)")]
        [MaxLength(11)]
        [Display(Name = "Your Mobaile")]
        public string Mobaile { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [MaxLength(190)]
        [Required(ErrorMessage = "Please Enter Your  Email Address.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Emaile { get; set; }


        [Column(TypeName = "nvarchar(Max)")]
        [MaxLength(200)]
        [Required(ErrorMessage = "Please EnterYour Message.")]
        public string Msg { get; set; }
    }
}
