using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class Contact
    {
        [Key]
        public int CoId { get; set; }
        [Required(ErrorMessage = "Le champ CoId est obligatoire.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Le champ Name est obligatoire.")]
        [EmailAddress]
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
