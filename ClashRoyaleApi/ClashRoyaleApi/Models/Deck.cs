using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClashRoyaleApi.Models;
public class Deck
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Jogador")]
    public int IdJogador { get; set; }
    public string NomeDeck { get; set; }
    public bool EhAtivo { get; set; }
}
