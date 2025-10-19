using Microsoft.EntityFrameworkCore;
using ClashRoyaleApi.Data;
using ClashRoyaleApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace ClashRoyaleApi.EndPoints;

public static class ArenaEndpoints
{
    public static void MapArenaEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Arena").WithTags(nameof(Arena));

        group.MapGet("/", async (ClashRoyaleApiContext db) =>
        {
            return await db.Arena.ToListAsync();
        })
        .WithName("GetAllArenas")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Arena>, NotFound>> (int id, ClashRoyaleApiContext db) =>
        {
            return await db.Arena.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Arena model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetArenaById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Arena arena, ClashRoyaleApiContext db) =>
        {
            var affected = await db.Arena
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, arena.Id)
                    .SetProperty(m => m.Nome, arena.Nome)
                    .SetProperty(m => m.TrofeusMinimos, arena.TrofeusMinimos)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateArena")
        .WithOpenApi();

        group.MapPost("/", async (Arena arena, ClashRoyaleApiContext db) =>
        {
            db.Arena.Add(arena);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Arena/{arena.Id}",arena);
        })
        .WithName("CreateArena")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, ClashRoyaleApiContext db) =>
        {
            var affected = await db.Arena
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteArena")
        .WithOpenApi();
    }
}
