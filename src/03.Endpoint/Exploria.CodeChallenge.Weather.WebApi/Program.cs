using Exploria.CodeChallenge.Weather.Infrustructure;
using Exploria.CodeChallenge.Weather.Infrustructure.Provider;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.RegisterHTTPClient<MeteorologyInquiryProvider>((p, client) =>
//{
//  //  var options = p.GetRequiredService<IOptions<UrlOption>>();
//    client.BaseAddress = builder.Configuration.GetSection("BaseUrlProvider").to;
//});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
