using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using supplier_service.Data;
using supplier_service.Models;
using StackExchange.Redis;

namespace supplier_service.Controllers;

[ApiController]
[Route("suppliers")]
public class SuppliersController : ControllerBase
{
    private readonly SupplierDbContext _context;

    public SuppliersController(SupplierDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll( int page = 1, int limit = 10)
    {
        var suppliers = await _context.Suppliers
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        return Ok(suppliers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null) return NotFound(new { message = "Поставщик не найден" });

        return Ok(supplier);
    }

    [HttpPost]
    public async Task<IActionResult> Create( Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = supplier.supplier_id }, supplier);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Supplier updated)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null) return NotFound(new { message = "Поставщик не найден" });

        supplier.name = updated.name;
        supplier.contact_person = updated.contact_person;
        supplier.email = updated.email;
        supplier.phone = updated.phone;
        supplier.status = updated.status;

        await _context.SaveChangesAsync();
        return Ok(supplier);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null) return NotFound(new { message = "Поставщик не найден" });

        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpGet("{id}/items")]
    public async Task<IActionResult> GetSupplierItems(int id)
    {
        var exists = await _context.Suppliers.AnyAsync(s => s.supplier_id == id);
        if (!exists)
        {
            return NotFound(new { message = "Поставщик не найден" });
        }
        var items = new[]
        {
            new
            {
                Id = 1,
                Name = "Mock Laptop",
                Article = "LP001",
                Category = "Electronics",
                Quantity = 10,
                Location = "Main Warehouse",
                Status = "available"
            },
                new
            {
                Id = 2,
                Name = "Mock Monitor",
                Article = "MN001",
                Category = "Electronics",
                Quantity = 5,
                Location = "Main Warehouse",
                Status = "available"
            }
        };
        return Ok(items);
    }

}
