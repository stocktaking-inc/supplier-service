using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace supplier_service.Models;

public class Good
{
    [Key]
    public int id { get; set; }

    [Required]
    public int supplier_id { get; set; }

    [Required]
    public int item_id { get; set; }

    [Required]
    public int warehouse_id { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal purchase_price { get; set; }

    [Required]
    public DateTime received_date { get; set; } = DateTime.UtcNow;

    public Supplier? Supplier { get; set; }
    public Item? Item { get; set; }
    public Warehouse? Warehouse { get; set; }
}