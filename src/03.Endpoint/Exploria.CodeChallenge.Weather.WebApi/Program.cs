using Exploria.CodeChallenge.Weather.Infrustructure;
using Exploria.CodeChallenge.Weather.Infrustructure.Provider;
using Exploria.CodeChallenge.Weather.Infrustructure.Provider.Contracts;
using Exploria.CodeChallenge.Weather.WebApi;
using Microsoft.Extensions.Options;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddApplication();

builder.RegisterHTTPClient<MeteorologyInquiryProvider>((p, client) =>
{
    var options = p.GetRequiredService<IOptions<MeteorologyConfig>>();

});
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
