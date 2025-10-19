using Microsoft.EntityFrameworkCore;
using ClashRoyaleApi.Data;
using ClashRoyaleApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace ClashRoyaleApi.EndPoints;

public static class BatalhaEndpoints
{
    public static void MapBatalhaEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Batalha").WithTags(nameof(Batalha));

        group.MapGet("/", async (ClashRoyaleApiContext db) =>
        {
            return await db.Batalha.ToListAsync();
        })
        .WithName("GetAllBatalhas")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Batalha>, NotFound>> (long id, ClashRoyaleApiContext db) =>
        {
            return await db.Batalha.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Batalha model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBatalhaById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (long id, Batalha batalha, ClashRoyaleApiContext db) =>
        {
            var affected = await db.Batalha
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, batalha.Id)
                    .SetProperty(m => m.TipoBatalha, batalha.TipoBatalha)
                    .SetProperty(m => m.DataHora, batalha.DataHora)
                    .SetProperty(m => m.DuracaoSegundos, batalha.DuracaoSegundos)
                    .SetProperty(m => m.Resultado, batalha.Resultado)
                    .SetProperty(m => m.TrofeusGanhosPerdidos, batalha.TrofeusGanhosPerdidos)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBatalha")
        .WithOpenApi();

        group.MapPost("/", async (Batalha batalha, ClashRoyaleApiContext db) =>
        {
            db.Batalha.Add(batalha);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Batalha/{batalha.Id}",batalha);
        })
        .WithName("CreateBatalha")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (long id, ClashRoyaleApiContext db) =>
        {
            var affected = await db.Batalha
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBatalha")
        .WithOpenApi();
    }
}
