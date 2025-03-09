using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;

        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio?> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(User user)
        {
            return await _context.Portfolios.Where(u => u.UserID == user.Id)
                .Select(stock => new Stock
                {
                    Id = stock.StockID,
                    Symbol = stock.Stock!.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    Purchase = stock.Stock.Purchase,
                    LastDividend = stock.Stock.LastDividend,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap
                }).ToListAsync();
        }

        public async Task<Portfolio?> DeleteAsync(User user, string symbol)
        {
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(p => p.UserID == user.Id && string.Equals(p!.Stock!.Symbol.ToLower(), symbol.ToLower()));
            if (portfolio is null) return null;

            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }
    }
}