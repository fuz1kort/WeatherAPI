using Service_C.Data;
using Service_C.Interfaces;
using Service_C.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddScoped<IWeatherService, Service_C.Services.WeatherService>();
builder.Services.AddSingleton<IWeatherStorage, WeatherStorage>();
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.MapGrpcService<WeatherServiceImpl>();
app.MapControllers();

app.Run();