using API.Extensions;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Diagnostics;

namespace API.Controllers
{
    [ApiController]
    [Route("api/portfolios")]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(UserManager<User> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username!);
            var portfolios = await _portfolioRepository.GetUserPortfolio(user!);
            return Ok(portfolios);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username!);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if (stock is null) return BadRequest("Stock not found");

            var portfolios = await _portfolioRepository.GetUserPortfolio(user!);
            if (portfolios.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio");

            var portfolioModel = new Portfolio
            {
                StockID = stock.Id,
                UserID = user!.Id,
            };
            await _portfolioRepository.CreateAsync(portfolioModel);
            if (portfolioModel is null)
            {
                return StatusCode(400, "Could not create");
            }

            return Created();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            var portfolio = await _portfolioRepository.GetUserPortfolio(user!);
            var filteredStocks = portfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower());

            if (!filteredStocks.Any())
                return BadRequest("Stock not in portfolio");

            await _portfolioRepository.DeleteAsync(user!, symbol);
            return Ok();
        }
    }
}