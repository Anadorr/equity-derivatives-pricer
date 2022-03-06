using EquityDerivativesPricer.Domain.Services.Calculators;
using EquityDerivativesPricer.Domain.Services.Pricers.VanillaOptions;
using EquityDerivativesPricer.Domain.Services.Pricers.VanillaOptions.Analytic;
using EquityDerivativesPricer.Domain.Services.Pricers.VanillaOptions.Binomial;
using EquityDerivativesPricer.Domain.Services.Pricers.VanillaOptions.FiniteDifferences;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IVanillaOptionPricerFactory, VanillaOptionPricerFactory>();
builder.Services.AddScoped<IVanillaOptionAnalyticPricer, VanillaOptionAnalyticPricer>();
builder.Services.AddScoped<IVanillaOptionBinomialPricer, VanillaOptionBinomialPricer>();
builder.Services.AddScoped<IVanillaOptionFiniteDifferencePricer, VanillaOptionFiniteDifferencePricer>();
builder.Services.AddScoped<IInterestRateCalculator, InterestRateCalculator>();

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
