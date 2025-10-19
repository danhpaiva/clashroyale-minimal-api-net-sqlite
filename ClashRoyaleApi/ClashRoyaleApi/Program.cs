using Microsoft.EntityFrameworkCore;
using ClashRoyaleApi.Data;
using ClashRoyaleApi.EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddDbContext<ClashRoyaleApiContext>(options =>
    options
    .UseSqlite(builder
    .Configuration
    .GetConnectionString("ClashRoyaleApiContext") ?? throw new InvalidOperationException("Connection string 'ClashRoyaleApiContext' not found.")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapArenaEndpoints();

app.MapBatalhaEndpoints();

app.MapCartaJogadorEndpoints();

app.MapClaEndpoints();

app.MapCartaEndpoints();

app.MapDeckEndpoints();

app.MapJogadorEndpoints();

app.MapParticipanteBatalhaEndpoints();

app.Run();
