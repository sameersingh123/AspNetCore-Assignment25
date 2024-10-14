using Assignment25.Models;
using Assignment25.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Assignment25.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly TradingOptions _tradingOptions;
        public TradeController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions)
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions.Value;
        }

        [ Route("/")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.DefaultStockSymbol == null)
            {
                _tradingOptions.DefaultStockSymbol = "MSFT";
            }

            Dictionary<string, object>? responseDictionary =await _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);
            Dictionary<string, object>? responseDictionary2 = await _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);

            StockTrade stockTrade = new StockTrade()
            {
                StockSymbol = _tradingOptions.DefaultStockSymbol,
                StockName= Convert.ToString(responseDictionary2["name"]),
                Price = Convert.ToDouble(responseDictionary["c"].ToString()),
                Quantity =Convert.ToDouble(responseDictionary2["shareOutstanding"].ToString()),
            };


            return View(stockTrade);
        }
    }
}
