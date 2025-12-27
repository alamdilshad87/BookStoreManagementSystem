using System.ComponentModel.DataAnnotations;

namespace BookStoreManagementSystem.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public virtual Book Book { get; set; }
    }
}
