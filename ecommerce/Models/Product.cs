
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class Product
    {
        [Key]
        public int? ProId { get; set; }
        [Required(ErrorMessage = "Le champ ProId est obligatoire.")]

        public string ProName { get; set; }
        [Required(ErrorMessage = "Le champ ProName est obligatoire.")]

        public string Description { get; set; }
        [Required(ErrorMessage = "Le champ Description est obligatoire.")]

        [Column(TypeName = "decimal(18, 2)")]
        public Decimal Price { get; set; }
        public string ProImage { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public int CatId { get; set; }
        [ForeignKey("CatId")]
        public virtual Category Category { get; set; }
    }
}
