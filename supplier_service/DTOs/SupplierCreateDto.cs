using System.ComponentModel.DataAnnotations;

namespace supplier_service.DTOs;

public class SupplierCreateDto
{
    [Required] public string Name { get; set; } = default!;

    [Required] public string Contact_person { get; set; } = default!;

    [Required] public string Email { get; set; } = default!;

    [Required] public string Phone { get; set; } = default!;

    [Required]
    [RegularExpression("^(active|inactive)$")]
    public string Status { get; set; } = "active";
}