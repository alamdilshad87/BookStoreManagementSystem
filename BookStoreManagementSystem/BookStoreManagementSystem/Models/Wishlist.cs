using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreManagementSystem.Models
{
    public class Wishlist
    {
        [Key]
        public int WishlistId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
    }
}
