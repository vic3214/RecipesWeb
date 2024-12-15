using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{

    [Table("Recipe")]
    public class Recipe: BaseEntity
    {
        [Column("Title")]
        [StringLength(150)]
        [Required]
        public string Title {get; set; } 
        
        [Column("Link")]
        [StringLength(200)]
        [Required]
        public string Link {get; set; }
            
        [Column("Source")]
        [StringLength(20)]
        [Required]
        public string Source {get; set; }
            
        [Column("Site")]
        [StringLength(30)]
        [Required]
        public string Site {get; set; } 
        
    }
}
