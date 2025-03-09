using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<Portfolio?> CreateAsync(Portfolio protfolio);
        Task<List<Stock>> GetUserPortfolio(User user);
    }
}