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
    public class OrderItemOptionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderItemOptionsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // POST: api/OrderItemOptions
        [HttpPost]
        public async Task<ActionResult<OrderItemOptionDTO>> Create(OrderItemOptionDTO dto)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            if (!await _context.OrderItems.AnyAsync(x => x.Id == dto.OrderItemId))
                return NotFound("OrderItem no encontrado.");

            if (dto.OptionValueId.HasValue &&
                !await _context.OptionValues.AnyAsync(x => x.Id == dto.OptionValueId))
                return NotFound("OptionValue no encontrado.");

            var option = new OrderItemOption
            {
                OrderItemId = dto.OrderItemId,
                OptionValueId = dto.OptionValueId,
                CreatedBy = userId.Value,
                CreatedAt = DateTime.UtcNow
            };

            _context.OrderItemOptions.Add(option);
            await _context.SaveChangesAsync();

            dto.Id = option.Id; // para la respuesta
            return CreatedAtAction(nameof(Get), new { id = option.Id }, dto);
        }

        // GET: api/OrderItemOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemOptionDTO>> Get(int id)
        {
            var option = await _context.OrderItemOptions.FindAsync(id);
            if (option == null)
                return NotFound();

            return new OrderItemOptionDTO
            {
                Id = option.Id,
                OrderItemId = option.OrderItemId,
                OptionValueId = option.OptionValueId
            };
        }

        // PUT: api/OrderItemOptions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderItemOptionDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var option = await _context.OrderItemOptions.FindAsync(id);
            if (option == null)
                return NotFound();

            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            if (dto.OptionValueId.HasValue &&
                !await _context.OptionValues.AnyAsync(x => x.Id == dto.OptionValueId))
                return NotFound("OptionValue no encontrado.");

            option.OptionValueId = dto.OptionValueId;
            option.UpdatedBy = userId.Value;
            option.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        private int? GetUserId()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdStr, out int userId) ? userId : null;
        }
    }
}