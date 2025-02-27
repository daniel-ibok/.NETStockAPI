using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("portfolios")]
    public class Portfolio
    {
        public uint UserID { get; set; }
        public uint StockID { get; set; }
        public User User { get; set; }
        public Stock Stock { get; set; }
    }
}