namespace supplier_service.DTOs;

public class SupplierResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Contact_person { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string Status { get; set; } = default!;
}