using System;
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
    public class RolesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        // Constructor
        public RolesController(AppDbContext context, IMapper mapper)
        {
            // Initialize the context and mapper
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            var rolesDTO = _mapper.Map<List<RoleDTO>>(roles);
            return Ok(rolesDTO);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDTO>> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return NotFound();

            var roleDTO = _mapper.Map<RoleDTO>(role);
            return Ok(roleDTO);
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<RoleDTO>> CreateRole(RoleDTO roleDTO)
        {
            var role = _mapper.Map<Role>(roleDTO);
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            var resultDTO = _mapper.Map<RoleDTO>(role);
            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, resultDTO);
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, RoleDTO roleDTO)
        {
            if (id != roleDTO.Id)
            return BadRequest();

            // Buscar el rol existente en la base de datos
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return NotFound();

            // Mapear los datos recibidos al objeto role existente (sin sobreescribir UpdatedBy aún)
            _mapper.Map(roleDTO, role);

            // Obtener el id del usuario logueado desde el token
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized("Usuario no autenticado o token inválido.");
            }

            // Asignar UpdatedBy con el usuario actual
            role.UpdatedBy = userId;
            role.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Roles.Any(r => r.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return NotFound();

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}