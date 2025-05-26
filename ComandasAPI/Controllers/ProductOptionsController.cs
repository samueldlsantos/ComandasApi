using System.Security.Claims;
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
    public class ProductOptionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductOptionsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ProductOptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductOptionDTO>>> GetAll()
        {
            var options = await _context.ProductOptions
                .Include(po => po.Values)
                .ToListAsync();

            return Ok(_mapper.Map<List<ProductOptionDTO>>(options));
        }

        // GET: api/ProductOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOptionDTO>> Get(int id)
        {
            var option = await _context.ProductOptions
                .Include(po => po.Values)
                .FirstOrDefaultAsync(po => po.Id == id);

            if (option == null)
                return NotFound();

            return Ok(_mapper.Map<ProductOptionDTO>(option));
        }

        // POST: api/ProductOptions
        [HttpPost]
        public async Task<ActionResult<ProductOptionDTO>> Create(ProductOptionDTO dto)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var entity = _mapper.Map<ProductOption>(dto);
            entity.CreatedBy = userId.Value;
            entity.CreatedAt = DateTime.UtcNow;

            _context.ProductOptions.Add(entity);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<ProductOptionDTO>(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, resultDto);
        }

        // PUT: api/ProductOptions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductOptionDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var entity = await _context.ProductOptions
                .Include(po => po.Values)
                .FirstOrDefaultAsync(po => po.Id == id);

            if (entity == null)
                return NotFound();

            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            // Solo se actualiza Name y ProductId, no valores aquí
            entity.Name = dto.Name;
            entity.ProductId = dto.ProductId;
            entity.UpdatedBy = userId.Value;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/ProductOptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.ProductOptions.FindAsync(id);
            if (entity == null)
                return NotFound();

            _context.ProductOptions.Remove(entity);
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
