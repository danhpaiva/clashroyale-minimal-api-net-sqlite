using Microsoft.EntityFrameworkCore;
using ClashRoyaleApi.Data;
using ClashRoyaleApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace ClashRoyaleApi.EndPoints;

public static class ClaEndpoints
{
    public static void MapClaEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Cla").WithTags(nameof(Cla));

        group.MapGet("/", async (ClashRoyaleApiContext db) =>
        {
            return await db.Cla.ToListAsync();
        })
        .WithName("GetAllClas")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Cla>, NotFound>> (int id, ClashRoyaleApiContext db) =>
        {
            return await db.Cla.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Cla model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetClaById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Cla cla, ClashRoyaleApiContext db) =>
        {
            var affected = await db.Cla
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, cla.Id)
                    .SetProperty(m => m.IdLider, cla.IdLider)
                    .SetProperty(m => m.NomeCla, cla.NomeCla)
                    .SetProperty(m => m.TrofeusMinimos, cla.TrofeusMinimos)
                    .SetProperty(m => m.TipoAdesao, cla.TipoAdesao)
                    .SetProperty(m => m.PontosGuerra, cla.PontosGuerra)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCla")
        .WithOpenApi();

        group.MapPost("/", async (Cla cla, ClashRoyaleApiContext db) =>
        {
            db.Cla.Add(cla);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Cla/{cla.Id}",cla);
        })
        .WithName("CreateCla")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, ClashRoyaleApiContext db) =>
        {
            var affected = await db.Cla
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCla")
        .WithOpenApi();
    }
}
