using Service_A.Configurations;
using Service_A.Interfaces;
using Service_A.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.Configure<WeatherApiOptions>(builder.Configuration.GetSection("WeatherApi"));
builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection("Kafka"));
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IWeatherProvider, WeatherProvider>();
builder.Services.AddSingleton<IKafkaProducer>(sp => new KafkaProducer("localhost:9092"));
builder.Services.AddHostedService<WeatherProducerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();