namespace Assignment25.ServiceContracts
{
    public interface IFinnhubService
    {
        Task<Dictionary<string, object>?> GetCompanyProfile(string StockSymbol);
        Task<Dictionary<string, object>?> GetStockPriceQuote(string StockSymbol);
    }
}
