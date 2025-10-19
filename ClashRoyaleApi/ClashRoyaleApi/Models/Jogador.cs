using System.ComponentModel.DataAnnotations;

namespace ClashRoyaleApi.Models;

public class Jogador
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "O campo NomeUsuario é obrigatorio!")]
    public string NomeUsuario { get; set; }
    public int Nivel { get; set; }
    public int Trofeus { get; set; }
    public int Gemas { get; set; }
    public int Ouro { get; set; }
}
