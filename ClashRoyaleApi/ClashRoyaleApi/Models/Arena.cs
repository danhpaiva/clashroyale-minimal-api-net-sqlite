using System.ComponentModel.DataAnnotations;

namespace ClashRoyaleApi.Models;

public class Arena
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public int TrofeusMinimos { get; set; }
}
