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
    public class OptionValuesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OptionValuesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/OptionValues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OptionValueDTO>>> GetOptionValues()
        {
            var optionValues = await _context.OptionValues.ToListAsync();
            var optionValuesDTO = _mapper.Map<List<OptionValueDTO>>(optionValues);
            return Ok(optionValuesDTO);
        }

        // GET: api/OptionValues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OptionValueDTO>> GetOptionValue(int id)
        {
            var optionValue = await _context.OptionValues.FindAsync(id);
            if (optionValue == null)
                return NotFound();

            var optionValueDTO = _mapper.Map<OptionValueDTO>(optionValue);
            return Ok(optionValueDTO);
        }

        // POST: api/OptionValues
        [HttpPost]
        public async Task<ActionResult<OptionValueDTO>> CreateOptionValue(OptionValueDTO optionValueDTO)
        {
            var optionValue = _mapper.Map<OptionValue>(optionValueDTO);

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId))
                return Unauthorized("Usuario no autenticado o token inválido.");

            optionValue.CreatedBy = userId;
            optionValue.CreatedAt = DateTime.UtcNow;

            _context.OptionValues.Add(optionValue);
            await _context.SaveChangesAsync();

            var resultDTO = _mapper.Map<OptionValueDTO>(optionValue);
            return CreatedAtAction(nameof(GetOptionValue), new { id = optionValue.Id }, resultDTO);
        }

        // PUT: api/OptionValues/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOptionValue(int id, OptionValueDTO optionValueDTO)
        {
            if (id != optionValueDTO.Id)
                return BadRequest();

            var optionValue = await _context.OptionValues.FindAsync(id);
            if (optionValue == null)
                return NotFound();

            _mapper.Map(optionValueDTO, optionValue);

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId))
                return Unauthorized("Usuario no autenticado o token inválido.");

            optionValue.UpdatedBy = userId;
            optionValue.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.OptionValues.Any(o => o.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/OptionValues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOptionValue(int id)
        {
            var optionValue = await _context.OptionValues.FindAsync(id);
            if (optionValue == null)
                return NotFound();

            _context.OptionValues.Remove(optionValue);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}