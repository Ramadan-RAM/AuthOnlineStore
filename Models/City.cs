using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthStore.Models
{
    [Table("City", Schema = "dbo")]
    public class City
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "City ID")]
        public int CityId { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        [Display(Name = "Name City")]
        public string CityName { get; set; }


        [Column(TypeName = "varchar(5)")]
        [Display(Name = "City Abbreviation")]
        public string CityAbbr { get; set; }

     
    }
}
