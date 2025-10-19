using Microsoft.EntityFrameworkCore;
using ClashRoyaleApi.Data;
using ClashRoyaleApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace ClashRoyaleApi.EndPoints;

public static class CartaJogadorEndpoints
{
    public static void MapCartaJogadorEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/CartaJogador").WithTags(nameof(CartaJogador));

        group.MapGet("/", async (ClashRoyaleApiContext db) =>
        {
            return await db.CartaJogador.ToListAsync();
        })
        .WithName("GetAllCartaJogadors")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<CartaJogador>, NotFound>> (int id, ClashRoyaleApiContext db) =>
        {
            return await db.CartaJogador.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is CartaJogador model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCartaJogadorById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, CartaJogador cartaJogador, ClashRoyaleApiContext db) =>
        {
            var affected = await db.CartaJogador
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, cartaJogador.Id)
                    .SetProperty(m => m.IdJogador, cartaJogador.IdJogador)
                    .SetProperty(m => m.IdCarta, cartaJogador.IdCarta)
                    .SetProperty(m => m.NivelAtual, cartaJogador.NivelAtual)
                    .SetProperty(m => m.QuantidadeCartas, cartaJogador.QuantidadeCartas)
                    .SetProperty(m => m.PossuiEvolucaoDesbloqueada, cartaJogador.PossuiEvolucaoDesbloqueada)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCartaJogador")
        .WithOpenApi();

        group.MapPost("/", async (CartaJogador cartaJogador, ClashRoyaleApiContext db) =>
        {
            db.CartaJogador.Add(cartaJogador);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/CartaJogador/{cartaJogador.Id}",cartaJogador);
        })
        .WithName("CreateCartaJogador")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, ClashRoyaleApiContext db) =>
        {
            var affected = await db.CartaJogador
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCartaJogador")
        .WithOpenApi();
    }
}
