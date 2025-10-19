using Microsoft.EntityFrameworkCore;

namespace ClashRoyaleApi.Data;

public class ClashRoyaleApiContext : DbContext
{
    public ClashRoyaleApiContext(DbContextOptions<ClashRoyaleApiContext> options)
        : base(options)
    {
    }

    public DbSet<ClashRoyaleApi.Models.Arena> Arena { get; set; } = default!;
    public DbSet<ClashRoyaleApi.Models.Batalha> Batalha { get; set; } = default!;
    public DbSet<ClashRoyaleApi.Models.Carta> Carta { get; set; } = default!;
    public DbSet<ClashRoyaleApi.Models.CartaJogador> CartaJogador { get; set; } = default!;
    public DbSet<ClashRoyaleApi.Models.Cla> Cla { get; set; } = default!;
    public DbSet<ClashRoyaleApi.Models.Deck> Deck { get; set; } = default!;
    public DbSet<ClashRoyaleApi.Models.Jogador> Jogador { get; set; } = default!;
    public DbSet<ClashRoyaleApi.Models.ParticipanteBatalha> ParticipanteBatalha { get; set; } = default!;
}
