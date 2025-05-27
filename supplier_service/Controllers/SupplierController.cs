using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using supplier_service.Data;
using supplier_service.DTOs;
using supplier_service.Models;

namespace supplier_service.Controllers;

[ApiController]
[Route("suppliers")]
[Authorize]
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

        var result = suppliers.Select(s => new SupplierResponseDto
        {
            Id = s.supplier_id,
            Name = s.name,
            Contact_person = s.contact_person,
            Email = s.email,
            Phone = s.phone,
            Status = s.status
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            return NotFound(new ErrorResponse
            {
                Message = "Поставщик не найден"
            });
        }

        var result = new SupplierResponseDto
        {
            Id = supplier.supplier_id,
            Name = supplier.name,
            Contact_person = supplier.contact_person,
            Email = supplier.email,
            Phone = supplier.phone,
            Status = supplier.status
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SupplierCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ErrorResponse
            {
                Message = "Неверный формат данных"
            });
        }

        var supplier = new Supplier
        {
            name = dto.Name,
            contact_person = dto.Contact_person,
            email = dto.Email,
            phone = dto.Phone,
            status = dto.Status
        };

        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();

        var response = new SupplierResponseDto
        {
            Id = supplier.supplier_id,
            Name = supplier.name,
            Contact_person = supplier.contact_person,
            Email = supplier.email,
            Phone = supplier.phone,
            Status = supplier.status
        };

        return CreatedAtAction(nameof(Get), new { id = supplier.supplier_id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SupplierUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ErrorResponse
            {
                Message = "Неверный формат данных"
            });
        }

        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            return NotFound(new ErrorResponse
            {
                Message = "Поставщик не найден"
            });
        }

        supplier.name = dto.Name;
        supplier.contact_person = dto.Contact_person;
        supplier.email = dto.Email;
        supplier.phone = dto.Phone;
        supplier.status = dto.Status;

        await _context.SaveChangesAsync();

        var result = new SupplierResponseDto
        {
            Id = supplier.supplier_id,
            Name = supplier.name,
            Contact_person = supplier.contact_person,
            Email = supplier.email,
            Phone = supplier.phone,
            Status = supplier.status
        };

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            return NotFound(new ErrorResponse
            {
                Message = "Поставщик не найден"
            });
        }

        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{id}/items")]
    public async Task<IActionResult> GetSupplierItems(int id)
    {
        var supplierExists = await _context.Suppliers.AnyAsync(s => s.supplier_id == id);
        if (!supplierExists)
        {
            return NotFound(new ErrorResponse
            {
                Message = "Поставщик не найден"
            });
        }

        try
        {
            var goods = await _context.Goods
                .Include(g => g.Item)
                .Include(g => g.Warehouse)
                .Where(g => g.supplier_id == id)
                .ToListAsync();

            var items = goods.Select(g => new ItemResponseDto
            {
                Id = g.Item!.id,
                Name = g.Item.name,
                Article = g.Item.article,
                Category = $"Category #{g.Item.category_id}",
                Quantity = g.quantity,
                Location = g.Warehouse?.name ?? "Неизвестно",
                Status = "available"
            });

            return Ok(items);
        }
        catch
        {
            return StatusCode(503, new ErrorResponse
            {
                Message = "Сервис товаров недоступен"
            });
        }
    }
}
