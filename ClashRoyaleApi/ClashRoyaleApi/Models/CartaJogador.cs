using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClashRoyaleApi.Models;

public class CartaJogador
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Jogador")]
    public int IdJogador { get; set; }
    [ForeignKey("Carta")]
    public int IdCarta { get; set; }
    public int NivelAtual { get; set; } 
    public int QuantidadeCartas { get; set; }
    public bool PossuiEvolucaoDesbloqueada { get; set; }
}
