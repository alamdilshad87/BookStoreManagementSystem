using System.ComponentModel.DataAnnotations;

namespace BookStoreManagementSystem.Models
{
    public class Wishlist
    {
        [Key]
        public int WishlistId { get; set; }

        public int UserId { get; set; }
        public int BookId { get; set; }

        public virtual Book Book { get; set; }
    }
}
