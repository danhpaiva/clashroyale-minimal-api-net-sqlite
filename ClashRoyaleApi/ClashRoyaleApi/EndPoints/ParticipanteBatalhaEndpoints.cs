using Microsoft.EntityFrameworkCore;
using ClashRoyaleApi.Data;
using ClashRoyaleApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace ClashRoyaleApi.EndPoints;

public static class ParticipanteBatalhaEndpoints
{
    public static void MapParticipanteBatalhaEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/ParticipanteBatalha").WithTags(nameof(ParticipanteBatalha));

        group.MapGet("/", async (ClashRoyaleApiContext db) =>
        {
            return await db.ParticipanteBatalha.ToListAsync();
        })
        .WithName("GetAllParticipanteBatalhas")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<ParticipanteBatalha>, NotFound>> (int id, ClashRoyaleApiContext db) =>
        {
            return await db.ParticipanteBatalha.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is ParticipanteBatalha model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetParticipanteBatalhaById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, ParticipanteBatalha participanteBatalha, ClashRoyaleApiContext db) =>
        {
            var affected = await db.ParticipanteBatalha
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, participanteBatalha.Id)
                    .SetProperty(m => m.IdBatalha, participanteBatalha.IdBatalha)
                    .SetProperty(m => m.IdJogador, participanteBatalha.IdJogador)
                    .SetProperty(m => m.IdDeckUsado, participanteBatalha.IdDeckUsado)
                    .SetProperty(m => m.CoroasConquistadas, participanteBatalha.CoroasConquistadas)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateParticipanteBatalha")
        .WithOpenApi();

        group.MapPost("/", async (ParticipanteBatalha participanteBatalha, ClashRoyaleApiContext db) =>
        {
            db.ParticipanteBatalha.Add(participanteBatalha);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/ParticipanteBatalha/{participanteBatalha.Id}",participanteBatalha);
        })
        .WithName("CreateParticipanteBatalha")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, ClashRoyaleApiContext db) =>
        {
            var affected = await db.ParticipanteBatalha
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteParticipanteBatalha")
        .WithOpenApi();
    }
}
