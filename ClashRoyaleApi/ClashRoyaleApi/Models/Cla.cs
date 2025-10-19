using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClashRoyaleApi.Models;

public class Cla
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Jogador")]
    public int IdLider { get; set; }
    public string NomeCla { get; set; }
    public int TrofeusMinimos { get; set; }
    public string TipoAdesao { get; set; } // Ex: "Aberto", "ApenasConvite"
    public int PontosGuerra { get; set; }
}
