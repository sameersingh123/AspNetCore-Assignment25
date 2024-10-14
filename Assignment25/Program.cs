using Assignment25;
using Assignment25.ServiceContracts;
using Assignment25.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
