using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace supplier_service.Models;

public class Good
{
    [Key]
    public int id { get; set; }

    [Required]
    public int supplierId { get; set; }

    [Required]
    public int itemId { get; set; }

    [Required]
    public int warehouseId { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal purchasePrice { get; set; }

    [Required]
    public DateTime receivedDate { get; set; } = DateTime.UtcNow;

    public Supplier? Supplier { get; set; }
    public Item? Item { get; set; }
    public Warehouse? Warehouse { get; set; }
}