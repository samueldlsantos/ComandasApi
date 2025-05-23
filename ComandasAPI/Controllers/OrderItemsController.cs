using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ComandasAPI.Data;
using ComandasAPI.DTO;
using ComandasAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComandasAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderItemsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/OrderItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetOrderItems()
        {
            var items = await _context.OrderItems
                .Include(i => i.Options)
                .ToListAsync();

            return Ok(_mapper.Map<List<OrderItemDTO>>(items));
        }

        // GET: api/OrderItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDTO>> GetOrderItem(int id)
        {
            var item = await _context.OrderItems
                .Include(i => i.Options)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
                return NotFound();

            return Ok(_mapper.Map<OrderItemDTO>(item));
        }

        // POST: api/OrderItems
        [HttpPost]
        public async Task<ActionResult<OrderItemDTO>> CreateOrderItem(OrderItemDTO dto)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId))
                return Unauthorized("Usuario no autenticado o token inválido.");

            var item = _mapper.Map<OrderItem>(dto);
            item.CreatedBy = userId;
            item.CreatedAt = DateTime.UtcNow;

            // Asignar metadatos a opciones
            foreach (var option in item.Options)
            {
                option.CreatedBy = userId;
                option.CreatedAt = DateTime.UtcNow;
            }

            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderItem), new { id = item.Id }, _mapper.Map<OrderItemDTO>(item));
        }

        // PUT: api/OrderItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItemDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var item = await _context.OrderItems
                .Include(i => i.Options)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
                return NotFound();

            _mapper.Map(dto, item);

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId))
                return Unauthorized("Usuario no autenticado o token inválido.");

            item.UpdatedBy = userId;
            item.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/OrderItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}