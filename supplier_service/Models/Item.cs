using System.ComponentModel.DataAnnotations;
namespace supplier_service.Models;

public class Item
{
    [Key]
    public int id { get; set; }

    [Required]
    [MaxLength(100)]
    public string name { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string article { get; set; } = default!;

    [Required]
    public int categoryId { get; set; }

    public ICollection<Good>? Goods { get; set; }
}