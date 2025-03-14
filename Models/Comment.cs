using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        public Stock? Stock { get; set; } // Navigation property

        public string? UserID { get; set; }
        public User? User { get; set; }
    }
}