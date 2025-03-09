using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("portfolios")]
    public class Portfolio
    {
        public string UserID { get; set; } = string.Empty;
        public int StockID { get; set; }
        public User? User { get; set; }
        public Stock? Stock { get; set; }
    }
}