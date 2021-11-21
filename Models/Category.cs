using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;



namespace AuthStore.Models
{
    [Table("Category", Schema = "dbo")]
    public partial class Category
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Category")]
        public int CategorId { get; set; }

        [Required(ErrorMessage = "Please Enter Category.")]
        [Column(TypeName = "nvarchar(150)")]
        [MaxLength(145)]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }
}
