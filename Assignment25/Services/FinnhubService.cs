using Assignment25.ServiceContracts;
using System.Text.Json;

namespace Assignment25.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<Dictionary<string, object>?> GetCompanyProfile(string StockSymbol)
        {
            using(HttpClient httpClient=_httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={StockSymbol}&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage httpResponseMessage=await httpClient.SendAsync(httpRequestMessage);

                Stream stream = httpResponseMessage.Content.ReadAsStream();
                StreamReader reader=new StreamReader(stream);
                string response = reader.ReadToEnd();

                Dictionary<string, object>? responseDictionary=JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if(responseDictionary == null)
                {
                    throw new InvalidOperationException("No response from Finnhub Server");
                }
                if (responseDictionary.ContainsKey("error")) {
                    throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));
                }

                return responseDictionary;
            }
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string StockSymbol)
        {
            using(HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={StockSymbol}&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage httpResponseMessage=await httpClient.SendAsync(httpRequestMessage);

                Stream stream = httpResponseMessage.Content.ReadAsStream();
                StreamReader streamReader=new StreamReader(stream);
                string response=streamReader.ReadToEnd();

                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if (responseDictionary == null)
                {
                    throw new InvalidOperationException("No response from Finnhub Server");
                }
                if (responseDictionary.ContainsKey("error"))
                {
                    throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));
                }

                return responseDictionary;



            }
        }
    }
}
