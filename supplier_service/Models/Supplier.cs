using System.ComponentModel.DataAnnotations;
namespace supplier_service.Models;

public class Supplier
{
    public int supplier_id { get; set; }
    [Required]
    public string name { get; set; } = default!;
    
    public string? contact_person { get; set; }

    public string? email { get; set; }

    public string? phone { get; set; }
    [Required]
    public string status { get; set; } = "active";

    public ICollection<Good>? Goods { get; set; }
}
