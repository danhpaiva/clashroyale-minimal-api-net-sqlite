using System.ComponentModel.DataAnnotations;

namespace ClashRoyaleApi.Models;

public class Batalha
{
    [Key]
    public long Id { get; set; }
    public string TipoBatalha { get; set; } // Ex: "1v1", "2v2", "Desafio"
    public DateTime DataHora { get; set; }
    public int DuracaoSegundos { get; set; } // Duração total em segundos
    public string Resultado { get; set; }     // Ex: "VITORIA", "DERROTA", "EMPATE"
    public int TrofeusGanhosPerdidos { get; set; }
}
