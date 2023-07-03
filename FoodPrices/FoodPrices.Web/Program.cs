using FoodPrices.Services.DelegatingHandlers;
using FoodPrices.Services.Options;
using FoodPrices.Services.Services;
using FoodPrices.Web.MappingProfiles;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<UrnerBarryAuthenticationHandler>();
builder.Services.AddTransient<ICurrencyService, CurrencyService>();
builder.Services.AddTransient<ICurrencyRatesRepo, CurrencyRatesRepo>();
builder.Services.AddHttpClient<IFoodIndexService, FoodIndexService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["UrnerBarry:BaseUrl"]);
}).AddHttpMessageHandler<UrnerBarryAuthenticationHandler>();

// Options
builder.Services.Configure<UrnerBarryOptions>(builder.Configuration.GetSection("UrnerBarry"));
builder.Services.Configure<CurrencyRatesOptions>(builder.Configuration.GetSection("CurrencyRates"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddAutoMapper(typeof(FoodIndexProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
