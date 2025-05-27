namespace supplier_service.DTOs;

public class ItemResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Article { get; set; } = default!;
    public string Category { get; set; } = default!;
    public int Quantity { get; set; }
    public string Location { get; set; } = default!;
    public string Status { get; set; } = "available";
}