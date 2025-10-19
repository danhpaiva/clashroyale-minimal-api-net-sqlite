using Microsoft.EntityFrameworkCore;
using ClashRoyaleApi.Data;
using ClashRoyaleApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace ClashRoyaleApi.EndPoints;

public static class CartaEndpoints
{
    public static void MapCartaEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Carta").WithTags(nameof(Carta));

        group.MapGet("/", async (ClashRoyaleApiContext db) =>
        {
            return await db.Carta.ToListAsync();
        })
        .WithName("GetAllCarta")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Carta>, NotFound>> (int id, ClashRoyaleApiContext db) =>
        {
            return await db.Carta.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Carta model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCartaById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Carta carta, ClashRoyaleApiContext db) =>
        {
            var affected = await db.Carta
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, carta.Id)
                    .SetProperty(m => m.Nome, carta.Nome)
                    .SetProperty(m => m.CustoElixir, carta.CustoElixir)
                    .SetProperty(m => m.Raridade, carta.Raridade)
                    .SetProperty(m => m.Tipo, carta.Tipo)
                    .SetProperty(m => m.Descricao, carta.Descricao)
                    .SetProperty(m => m.PossuiEvolucao, carta.PossuiEvolucao)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCarta")
        .WithOpenApi();

        group.MapPost("/", async (Carta carta, ClashRoyaleApiContext db) =>
        {
            db.Carta.Add(carta);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Carta/{carta.Id}",carta);
        })
        .WithName("CreateCarta")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, ClashRoyaleApiContext db) =>
        {
            var affected = await db.Carta
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCarta")
        .WithOpenApi();
    }
}
