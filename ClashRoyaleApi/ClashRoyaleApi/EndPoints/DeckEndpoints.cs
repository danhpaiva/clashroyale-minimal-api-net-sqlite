using Microsoft.EntityFrameworkCore;
using ClashRoyaleApi.Data;
using ClashRoyaleApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace ClashRoyaleApi.EndPoints;

public static class DeckEndpoints
{
    public static void MapDeckEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Deck").WithTags(nameof(Deck));

        group.MapGet("/", async (ClashRoyaleApiContext db) =>
        {
            return await db.Deck.ToListAsync();
        })
        .WithName("GetAllDecks")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Deck>, NotFound>> (int id, ClashRoyaleApiContext db) =>
        {
            return await db.Deck.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Deck model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetDeckById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Deck deck, ClashRoyaleApiContext db) =>
        {
            var affected = await db.Deck
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, deck.Id)
                    .SetProperty(m => m.IdJogador, deck.IdJogador)
                    .SetProperty(m => m.NomeDeck, deck.NomeDeck)
                    .SetProperty(m => m.EhAtivo, deck.EhAtivo)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateDeck")
        .WithOpenApi();

        group.MapPost("/", async (Deck deck, ClashRoyaleApiContext db) =>
        {
            db.Deck.Add(deck);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Deck/{deck.Id}",deck);
        })
        .WithName("CreateDeck")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, ClashRoyaleApiContext db) =>
        {
            var affected = await db.Deck
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteDeck")
        .WithOpenApi();
    }
}
