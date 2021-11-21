using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace AuthStore.Models
{ 
    [Table("Vistor", Schema = "dbo")]
    public partial class Vistor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Vist Id")]
        public int VistId { get; set; }

        [Column(TypeName = "int")]
        public int Number { get; set; }
    }
}
