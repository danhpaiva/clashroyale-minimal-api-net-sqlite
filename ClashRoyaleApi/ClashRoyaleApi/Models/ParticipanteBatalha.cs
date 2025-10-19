using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClashRoyaleApi.Models;

public class ParticipanteBatalha
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Batalha")]
    public int IdBatalha { get; set; }
    [ForeignKey("Jogador")]
    public int IdJogador { get; set; }
    [ForeignKey("Deck")]
    public int IdDeckUsado { get; set; }
    public int CoroasConquistadas { get; set; }
}
