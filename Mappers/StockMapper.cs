using API.Dtos.Stock;
using API.Models;

namespace API.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDTO(this Stock stock)
        {
            return new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDividend = stock.LastDividend,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
            };
        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stock)
        {
            return new Stock
            {
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDividend = stock.LastDividend,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap
            };
        }
    }
}