using Microsoft.EntityFrameworkCore;
using ClashRoyaleApi.Data;
using ClashRoyaleApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace ClashRoyaleApi.EndPoints;

public static class JogadorEndpoints
{
    public static void MapJogadorEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Jogador").WithTags(nameof(Jogador));

        group.MapGet("/", async (ClashRoyaleApiContext db) =>
        {
            return await db.Jogador.ToListAsync();
        })
        .WithName("GetAllJogadors")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Jogador>, NotFound>> (int id, ClashRoyaleApiContext db) =>
        {
            return await db.Jogador.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Jogador model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetJogadorById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Jogador jogador, ClashRoyaleApiContext db) =>
        {
            var affected = await db.Jogador
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, jogador.Id)
                    .SetProperty(m => m.NomeUsuario, jogador.NomeUsuario)
                    .SetProperty(m => m.Nivel, jogador.Nivel)
                    .SetProperty(m => m.Trofeus, jogador.Trofeus)
                    .SetProperty(m => m.Gemas, jogador.Gemas)
                    .SetProperty(m => m.Ouro, jogador.Ouro)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateJogador")
        .WithOpenApi();

        group.MapPost("/", async (Jogador jogador, ClashRoyaleApiContext db) =>
        {
            db.Jogador.Add(jogador);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Jogador/{jogador.Id}",jogador);
        })
        .WithName("CreateJogador")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, ClashRoyaleApiContext db) =>
        {
            var affected = await db.Jogador
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteJogador")
        .WithOpenApi();
    }
}
