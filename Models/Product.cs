
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AuthStore.Models
{
    [Table("Product", Schema = "dbo")]
    public partial class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please Enter Product No.")]
        [Column(TypeName = "nvarchar(20)")]
        [MaxLength(15)]
        [Display(Name = "Product No.")]
        public string ProductCode { get; set; }

        [ForeignKey("Category")]
        [Required]
        public int CategorId { get; set; }

        [Display(Name = "Category Name")]
        [NotMapped]
        public string CategoryName { get; set; }

        [ForeignKey(nameof(CategorId))]
        //[InverseProperty("Products")]
        public virtual Category Category { get; set; }


        [Required(ErrorMessage = "Please Enter Product Name.")]
        [Column(TypeName = "nvarchar(150)")]
        [MaxLength(145)]
        [Display(Name = "ProductName")]
        public string ProductName { get; set; }

        [Required]
        [Column(TypeName = ("decimal"))]
        [Display(Name = "Quantity")]
        public decimal Quantity { get; set; }

        [Required]
        [Column(TypeName = ("money"))]
        public decimal Cost { get; set; }

        [Required]
        [Column(TypeName = ("money"))]
        public decimal Price { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        [MaxLength(145)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Please Choose Front image.")]
        [Display(Name = "Front Image")]
        [NotMapped]
        public IFormFile FrontImage { get; set; }

        [Required]
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }


        //public string BackImageUrl { get; set; }
        //[Display(Name = "Back Image")]
        //[NotMapped]
        //public IFormFile BackImage { get; set; }

    }
}
