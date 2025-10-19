using System.ComponentModel.DataAnnotations;

namespace ClashRoyaleApi.Models;

public class Carta
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "O campo Nome é obrigatorio!")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O campo CustoElixir é obrigatorio!")]
    public int CustoElixir { get; set; }
    public string Raridade { get; set; }
    public string Tipo { get; set; }
    public string Descricao { get; set; }
    public bool PossuiEvolucao { get; set; }
}
