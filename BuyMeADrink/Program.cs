using BuyMeADrink.Services;
using BuyMeADrink.Utils;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var supabaseSettings = builder.Configuration.GetSection("Supabase");
var supabaseUrl = supabaseSettings["Url"];
var supabaseKey = supabaseSettings["AnonKey"];

builder.Services.AddSingleton(client => 
    new Client(supabaseUrl!, supabaseKey));

// Dependency Injection
builder.Services.ConfigureCommonServices();


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