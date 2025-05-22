using System.ComponentModel.DataAnnotations;
namespace supplier_service.Models;

public class Warehouse
{
    [Key]
    public int id { get; set; }

    [Required]
    [MaxLength(100)]
    public string name { get; set; } = default!;

    public string? address { get; set; }

    public bool isActive { get; set; } = true;

    public ICollection<Good>? Goods { get; set; }
}