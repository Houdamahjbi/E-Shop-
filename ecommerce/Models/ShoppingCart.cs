using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; } 
        public int ProId { get; set;}
        [ForeignKey("ProId")]
        public virtual Product Product { get; set; }    
        public decimal Price { get; set; }
        [Range(1,int.MaxValue)]
        public int Qty { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]  
        public virtual ApplicationUser ApplicationUser { get; set; }    
    }
}
